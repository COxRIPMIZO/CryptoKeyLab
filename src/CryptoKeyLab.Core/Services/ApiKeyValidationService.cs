using CryptoKeyLab.Core.Services.InternalCode.ApiKeyHashing;
using CryptoKeyLab.Domain.Interfaces;
using CryptoKeyLab.Domain.Interfaces.Caching;
using CryptoKeyLab.Domain.Interfaces.SystemInternal;
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
        private readonly IApiKeyRepository _apiKeyRepository;
        private readonly ISystemHashProvider _systemHashProvider;
        private readonly ICacheRepository _cacheRepository;

        //DI injection of the repository to access API key data
        public ApiKeyValidationService(IApiKeyRepository apikeyRepo,ISystemHashProvider systemHash,ICacheRepository cacheRepository)
        {
            _apiKeyRepository = apikeyRepo;
            _systemHashProvider = systemHash;
            _cacheRepository = cacheRepository;
        }
        public async Task<ApiKeyValidationResult> ValidateAndConsumeKeyAsync(string rawApiKey)
        {
            //===============================================
            //Step 1 : converting the incoming key into hash and check if it exist in the database or not
            //===============================================
            var keyIdentity = _systemHashProvider.ComputeHash(rawApiKey);
            
            // ==========================================================
            // STEP 1: THE CACHE-ASIDE PATTERN (Try Cache First)
            // ==========================================================
            var keyEntity = await _cacheRepository.GetAsync<ApiKeyEntity>(keyIdentity);

            if (keyEntity == null)
            {
                //===============================================
                //Step 2 : check in the database using Repository if the key exixt or not
                //===============================================
                keyEntity = await _apiKeyRepository.GetByKeyHashAsync(keyIdentity);

                if (keyEntity != null)
                {
                    // Save it to Cache so we don't hit SQL again for 5 minutes!
                    await _cacheRepository.SetAsync(keyIdentity, keyEntity, TimeSpan.FromMinutes(5));
                }
            }

            //===============================================
            //Step 2 : check in the database using Repository if the key exixt or not
            //===============================================
            //var keyEntity = await _apiKeyRepository.GetByKeyHashAsync(keyIdentity);

            //===============================================
            //Step 3 : Check the key validation (existence, expiration, active status)
            //===============================================
            if (keyEntity is null || !keyEntity.IsActive || keyEntity.ExpiresAt < DateTime.UtcNow)
            {
                return new ApiKeyValidationResult(false,false, "Invalid, inactive, or expired API Key.",null);
            }

            ////===============================================
            ////Step 4 : Check the key rateLimit
            ////===============================================
            //if(keyEntity.TotalUsageCount > keyEntity.RateLimitPerMinute)
            //{
            //    return new ApiKeyValidationResult(false, true, "Rate Limit Exceeded. Please upgrade your tier.", null);
            //}

            ////===============================================
            ////Step 5 : Update the key usage count in the database
            ////===============================================
            //await _apiKeyRepository.IncrementUsageAsync(keyEntity.Id);


            // ==========================================================
            // STEP 3: ATOMIC RATE LIMITING (Using Cache instead of SQL!)
            // ==========================================================
            // We track their usage in RAM/Redis. This is 100x faster than updating SQL every request.
            string rateLimitKey = $"RateLimit_{keyEntity.Id}";

            // Atomically increment the usage count. The window resets every 1 minute.
            long currentUsage = await _cacheRepository.IncrementAsync(rateLimitKey, TimeSpan.FromMinutes(1));

            if (currentUsage > keyEntity.RateLimitPerMinute)
            {
                return new ApiKeyValidationResult(false, true, "Rate Limit Exceeded. Please slow down.", null);
            }

            // ==========================================================
            // STEP 4: ASYNC DB BACKUP (Optional but recommended)
            // ==========================================================
            // We still increment SQL so your analytics are correct, 
            // but we don't 'await' it, or we let a background worker do it later!
            _ = _apiKeyRepository.IncrementUsageAsync(keyEntity.Id); // Fire and forget!

            //===============================================
            //Step 6 : return the validation result with the key entity details (without exposing sensitive information)
            //===============================================

            return new ApiKeyValidationResult(true,false,null,keyEntity);
        }
    }
}
