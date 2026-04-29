using CryptoKeyLab.LimitResetWorker.Interfaces;
using CryptoKeyLab.LimitResetWorker.Models;
using CryptoKeyLab.LimitResetWorker.Services.Shared;
using Microsoft.Extensions.Options;
using Serilog;

namespace CryptoKeyLab.LimitResetWorker
{
    public class LimitResetWorker : BackgroundService
    {
        private readonly IOptionsMonitor<ResetWorkerSettings> _optionsMonitor;
        private readonly IApiKeyGetService _apiKeyGetService;
        private readonly IApiKeyResetService _apiKeyResetService;
        public LimitResetWorker(IOptionsMonitor<ResetWorkerSettings> optionsMonitor, IApiKeyGetService apiKeyGetService,IApiKeyResetService apiKeyResetService)
        {
            _optionsMonitor = optionsMonitor;
            _apiKeyGetService = apiKeyGetService;
            _apiKeyResetService = apiKeyResetService;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            var startDelay = _optionsMonitor.CurrentValue.ServiceStartDelay.ToTimeSpan();

            await Task.Delay(startDelay, stoppingToken);
            Log.Information("LimitResetWorker is running at: {time}", DateTimeOffset.Now);

            while (!stoppingToken.IsCancellationRequested)
            {
                // 3. DYNAMIC WAIT (Read the config every time the loop finishes)
                // This makes your IOptionsMonitor actually work!
                var loopInterval = _optionsMonitor.CurrentValue.ServiceStartDelay.ToTimeSpan();

                try
                {
                    var apiKeys = await _apiKeyGetService.GetApiData();
                    var apiKeysList = apiKeys.ToList();
                    Log.Information("Fetched {count} API keys for reset.", apiKeys.Count());

                    // Process each API key and reset its usage count
                    if (!apiKeysList.Any())
                    {
                        Log.Information("No key found for reset.");

                        await Task.Delay(loopInterval, stoppingToken);

                        continue;
                    }

                    //process for reset and deactivation
                    var usageCount = apiKeysList.Where(P => P.TotalUsageCount > 0 && (DateTime.UtcNow - P.LastUsageReset).TotalMinutes >= P.RateLimitPerMinute);

                    //pass into update repo
                    if (usageCount.Any())
                    {
                        var resetIds = usageCount.Select(P => P.Id).ToList();
                        await _apiKeyResetService.BulkResetUsageCountsAsync(resetIds);

                        //log information
                        Log.Information("Reset usage count for {count} API keys.", resetIds.Count());
                    }

                    //process for deactivation
                    var deactiveKeys = apiKeysList.Where(P => P.ExpiresAt < DateTime.UtcNow);

                    if (deactiveKeys.Any())
                    {
                        var deactivetIds = deactiveKeys.Select(P => P.Id).ToList();
                        await _apiKeyResetService.BulkDeactivateExpiredKeysAsync(deactivetIds);

                        //log information
                        Log.Information("Deactivated {count} expired API keys.", deactivetIds.Count());
                    }
                }
                catch (Exception ex)
                {
                    Log.Error(ex, "An error occurred while fetching API key data.");
                }

                Log.Information("Cycle complete. Waiting {interval} until next run...", loopInterval);
                await Task.Delay(loopInterval, stoppingToken);
            }
        }
    }
}
