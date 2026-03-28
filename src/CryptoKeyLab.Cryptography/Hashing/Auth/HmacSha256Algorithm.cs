using CryptoKeyLab.Domain.Interfaces.Cryptography.Hash;
using CryptoKeyLab.Domain.Models.Cryptography;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace CryptoKeyLab.Cryptography.Hashing.Auth
{
    public class HmacSha256Algorithm : IHashAlgorithm
    {
        public string? Name => "HMAC-SHA256";

        public CryptoResult ComputeHash(HashOptions hashOptions)
        {
            // VALIDATION: If this algo requires a key, check if the user provided it!
            if (string.IsNullOrWhiteSpace(hashOptions.Key))
                throw new ArgumentException("HMAC requires a Secret Key.");

            var sw = Stopwatch.StartNew();

            // Convert the string key to bytes
            var keyBytes = Encoding.UTF8.GetBytes(hashOptions.Key);
            using var hmac = new HMACSHA256(keyBytes);

            var hashBytes = hmac.ComputeHash(Encoding.UTF8.GetBytes(hashOptions.Input));
            sw.Stop();

            return new CryptoResult(Convert.ToHexString(hashBytes).ToLower(), sw.Elapsed.TotalMilliseconds);
        }
    }
}
