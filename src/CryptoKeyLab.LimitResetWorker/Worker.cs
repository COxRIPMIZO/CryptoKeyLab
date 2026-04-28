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

            while (!stoppingToken.IsCancellationRequested)
            {
                await Task.Delay(startDelay, stoppingToken);

                Log.Information("LimitResetWorker is running at: {time}", DateTimeOffset.Now);

                try
                {
                    var apiKeys = await _apiKeyGetService.GetApiData();
                    Log.Information("Fetched {count} API keys for reset.", apiKeys.Count());

                    // Process each API key and reset its usage count
                    if (!apiKeys.Any())
                    {
                        Log.Information("No key found for reset.");
                        continue;
                    }

                    //process for reset and deactivation
                    //var usageCount = apiKeys.Where(P => P.TotalUsageCount > _optionsMonitor.CurrentValue.)
                }
                catch (Exception ex)
                {
                    Log.Error(ex, "An error occurred while fetching API key data.");
                }

                // 3. DYNAMIC WAIT (Read the config every time the loop finishes)
                // This makes your IOptionsMonitor actually work!
                var loopInterval = _optionsMonitor.CurrentValue.ServiceStartDelay.ToTimeSpan();

                Log.Information("Cycle complete. Waiting {interval} until next run...", loopInterval);
                await Task.Delay(loopInterval, stoppingToken);
            }
        }
    }
}
