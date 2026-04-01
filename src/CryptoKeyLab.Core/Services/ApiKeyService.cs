using CryptoKeyLab.Core.Services.InternalCode.ApiKeyHashing;
using CryptoKeyLab.Cryptography.Hashing.Cryptographic;
using CryptoKeyLab.Domain.Interfaces;
using CryptoKeyLab.Domain.Interfaces.Cryptography.Hash;
using CryptoKeyLab.Domain.Interfaces.SystemInternal;
using CryptoKeyLab.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace CryptoKeyLab.Core.Services
{
    public class ApiKeyService : IApiKeyService
    {
        //adding prefix for better security and to easily identify the key type
        private const string KeyPrefix = "Ckl_Temp_";

        //define key length here we uses 32 for better security , which is 256 bits binary (Military grade randomness)
        public const int KeyLengthBytes = 32;

        //define the expiration time for the temporary key (24 hours)
        private const int KeyExpirationHours = 24;

        private readonly IApiKeyRepository _apiKeyRepository;
        private readonly ISystemHashProvider _hashAlgorithm;

        //DI Inejectoion of repository to save the generated key details into database
        public ApiKeyService(IApiKeyRepository keyRepository,ISystemHashProvider hashAlgorithm)
        {
            _apiKeyRepository = keyRepository;
            _hashAlgorithm = hashAlgorithm;
        }

        public async Task<TemporarykeyResponse> GenerateTemporaryKeyAsync()
        {
            //===================================================================
            // Step 1: Generate a cryptographically secure random bytes
            //===================================================================
            var keyBytes = new byte[KeyLengthBytes];
            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(keyBytes);
            }

            //===================================================================
            // Step 2: Format it to be URL/Header safe (Base64Url encoding)
            //===================================================================
            string base64Key = Convert.ToBase64String(keyBytes).Replace("+","-").Replace("/","_").TrimEnd('=');

            //===================================================================
            // Step 3: Generate final api key with prefix
            //===================================================================
            string fullPlainTextKey = $"{KeyPrefix}{base64Key}";

            //===================================================================
            // Step 4: Creating model for leter saving into database or doing further processing
            //===================================================================
            var keyEntity = new ApiKeyEntity
            {
                Id = Guid.NewGuid(),
                keyPrefix = KeyPrefix,
                CreatedAt = DateTime.UtcNow,

                // In a real DB flow, you hash 'fullPlainTextKey' with SHA256 and store the hash here.
                //================================================
                //added on 01-04-2026 
                //use for security purpose we are hashing the key before storing it in database, so even if database is compromised the attacker will not get the actual key, they will only get the hash which is useless without the original key
                //================================================
                KeyHash = _hashAlgorithm.ComputeHash(fullPlainTextKey),//"HASHED_VALUE_FOR_DB",

                ExpiresAt = DateTime.UtcNow.AddHours(KeyExpirationHours),
                RateLimitPerMinute = 30 // Strict limit for free anonymous users
            };

            // 4. Save to Database using Dapper!
            await _apiKeyRepository.CreatekeyAsync(keyEntity);

            //===================================================================
            // Step 5: Return the plain text key to the caller (only at creation time, never store or return it again)
            //===================================================================

            return new TemporarykeyResponse
            (
                ApiKey: fullPlainTextKey,
                ExpireAt : keyEntity.ExpiresAt,
                Message: "Warning: This key will expire in 24 hours and will not be shown again. Do not share it.",
                RateLimitPerMinute : keyEntity.RateLimitPerMinute
            );
        }
    }
}
