using CryptoKeyLab.Domain.Interfaces.Cryptography.Hash;
using CryptoKeyLab.Domain.Models.Cryptography;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using Blake3;
using System.Threading.Tasks;

namespace CryptoKeyLab.Cryptography.Hashing.Cryptographic.Blake
{
    public class Blake3Algorithm : IHashAlgorithm
    {
        public string? Name => "BLAKE3";

        public CryptoResult ComputeHash(HashOptions hashOptions)
        {
            //Step 1.check for blank input
            if (string.IsNullOrWhiteSpace(hashOptions.Input))
                return new CryptoResult(string.Empty, 0);

            //intialize the stop watch
            Stopwatch sw = Stopwatch.StartNew();

            // 2. Optimized Hashing Logic
            // We use UTF8 to convert string to bytes. 
            var inputBytes = Encoding.UTF8.GetBytes(hashOptions.Input);

            // For performance, the 'Hasher.Hash' method is thread-safe and SIMD accelerated.
            //// BLAKE3 produces a 32-byte (256-bit) hash by default
            var blake3hash = Hasher.Hash(inputBytes);

            //step 3.convert the hash to hex string
            var resulthex = blake3hash.ToString();

            //stop the watch and return the result
            sw.Stop();

            return new CryptoResult(resulthex,sw.Elapsed.TotalMilliseconds);
        }
    }
}
