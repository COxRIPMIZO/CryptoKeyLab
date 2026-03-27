using CryptoKeyLab.Domain.Interfaces;
using CryptoKeyLab.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CryptoKeyLab.Core.Services
{
    public class ApiKeyValidationService : IApiKeyValidationService
    {
        public readonly IApiKeyRepository _apiKeyRepository;

        //DI injection of the repository to access API key data
        public ApiKeyValidationService(IApiKeyRepository apikeyRepo)
        {
            _apiKeyRepository = apikeyRepo;
        }
        public async Task<ApiKeyValidationResult> ValidateAndConsumeKeyAsync(string rawApiKey)
        {
            //===============================================
            //Step 1 : converting the incoming key into hash and check if it exist in the database or not
            //===============================================
            //var keyIdentity = conputingHash(rawApiKey);

            //===============================================
            //Step 2 : check in the database using Repository if the key exixt or not
            //===============================================
            var keyEntity = await _apiKeyRepository.GetByKeyHashAsync(rawApiKey);

            //===============================================
            //Step 3 : Check the key validation (existence, expiration, active status)
            //===============================================
            if (keyEntity is null || !keyEntity.IsActive || keyEntity.ExpiresAt < DateTime.UtcNow)
            {
                return new ApiKeyValidationResult(false,false, "Invalid, inactive, or expired API Key.",null);
            }

            //===============================================
            //Step 4 : Check the key rateLimit
            //===============================================
            if(keyEntity.TotalUsageCount > keyEntity.RateLimitPerMinute)
            {
                return new ApiKeyValidationResult(false, true, "Rate Limit Exceeded. Please upgrade your tier.", null);
            }

            //===============================================
            //Step 5 : Update the key usage count in the database
            //===============================================
            await _apiKeyRepository.IncrementUsageAsync(keyEntity.Id);

            //===============================================
            //Step 6 : return the validation result with the key entity details (without exposing sensitive information)
            //===============================================

            return new ApiKeyValidationResult(true,false,null,keyEntity);
        }
    }
}
