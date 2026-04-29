using CryptoKeyLab.LimitResetWorker.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CryptoKeyLab.LimitResetWorker.Services
{
    public class ApiKeyResetService : IApiKeyResetService
    {
        private readonly IApiKeyResetRepo _apiKeyResetRepo;
        public ApiKeyResetService(IApiKeyResetRepo apiKeyResetRepo)
        {
            _apiKeyResetRepo = apiKeyResetRepo ?? throw new ArgumentNullException(nameof(apiKeyResetRepo), "ApiKeyResetRepo cannot be null.");
        }
        public async Task BulkDeactivateExpiredKeysAsync(IEnumerable<Guid> keyIdsToDeactivate)
        {
            await _apiKeyResetRepo.SetKeysInactiveAsync(keyIdsToDeactivate);
        }

        public async Task BulkResetUsageCountsAsync(IEnumerable<Guid> keyIdsToReset)
        {
            await _apiKeyResetRepo.UpdateUsageCountsToZeroAsync(keyIdsToReset);
        }
    }
}
