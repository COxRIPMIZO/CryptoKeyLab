using CryptoKeyLab.LimitResetWorker.Interfaces;
using CryptoKeyLab.LimitResetWorker.Models;
using Microsoft.Extensions.Options;

namespace CryptoKeyLab.LimitResetWorker
{
    public class Worker: BackgroundService
    {
        private readonly ILogger<Worker> _logger;
        private readonly IOptionsMonitor<ResetWorkerSettings> _optionsMonitor;
        private readonly IApiKeyGetService _apiKeyGetService;
        public Worker(ILogger<Worker> logger, IOptionsMonitor<ResetWorkerSettings> optionsMonitor, IApiKeyGetService apiKeyGetService)
        {
            _logger = logger;
            _optionsMonitor = optionsMonitor;
            _apiKeyGetService = apiKeyGetService;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                if (_logger.IsEnabled(LogLevel.Information))
                {
                    _logger.LogInformation("Worker running at: {time}", DateTimeOffset.Now);
                }
                await Task.Delay(1000, stoppingToken);

                try
                {
                    var apiKeys = await _apiKeyGetService.GetApiData();
                    _logger.LogInformation("Fetched {count} API keys for reset.", apiKeys.Count());
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "An error occurred while fetching API key data.");
                }
            }
        }
    }
}
