using CryptoKeyLab.Domain.Interfaces.Cryptography.Hash;
using CryptoKeyLab.Domain.Models.Cryptography;
using CryptoKeyLab.Domain.Models.Cryptography.Hash;
using Org.BouncyCastle.Crypto.Digests;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace CryptoKeyLab.Cryptography.Hashing.Auth.HMAC
{
    public class HmacSha512Algorithm : IHashAlgorithm
    {
        public string? Name => "HMAC-SHA512";

        public CryptoResult ComputeHash(HashOptions hashOptions)
        {
            //check inpuot validation
            if (string.IsNullOrWhiteSpace(hashOptions.Input))
                throw new ArgumentException("Input cannot be empty.", nameof(hashOptions.Input));

            if (string.IsNullOrWhiteSpace(hashOptions.Key))
                throw new ArgumentException("Key cannot be empty.", nameof(hashOptions.Key));

            var keyBytes = Encoding.UTF8.GetBytes(hashOptions.Key);
            var inputBytes = Encoding.UTF8.GetBytes(hashOptions.Input);

            //step 1 .initialize the stop watch
            Stopwatch sw = Stopwatch.StartNew();

            //step 2.initilize the SHA1 algorithm
            using var hmacSHA512 = new HMACSHA512(keyBytes);

            //step 5.compute the digest bytes
            byte[] resultBytes = hmacSHA512.ComputeHash(inputBytes);
            
            sw.Stop();

            //step convert into string
            string res = Convert.ToHexString(resultBytes);

            return new CryptoResult(res, sw.Elapsed.TotalMilliseconds);
        }
    }
}
