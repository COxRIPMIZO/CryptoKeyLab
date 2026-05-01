using CryptoKeyLab.Domain.Interfaces.Cryptography.Hash;
using CryptoKeyLab.Domain.Models.Cryptography;
using CryptoKeyLab.Domain.Models.Cryptography.Hash;
using Org.BouncyCastle.Crypto.Digests;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CryptoKeyLab.Cryptography.Hashing.Lightweight
{
    public class AsconAlgorithm : IHashAlgorithm
    {
        public string? Name => "Ascon (Lightweight)";

        public CryptoResult ComputeHash(HashOptions hashOptions)
        {
            if(string.IsNullOrWhiteSpace(hashOptions.Input))
                throw new ArgumentException("Input must be provided for Ascon hashing.");

            //start stop watch
            Stopwatch sw = Stopwatch.StartNew();

            //step 1. Generate input bytes
            var inputBytes = Encoding.UTF8.GetBytes(hashOptions.Input);
            //var keyBytes = Encoding.UTF8.GetBytes(hashOptions.Key);
            //var saltBytes = Encoding.UTF8.GetBytes(hashOptions.Salt);

            //step 2. initialize the ascon engine with key and salt
            var engine = new AsconHash256();

            //step 3. update the engine with input bytes
            engine.BlockUpdate(inputBytes,0,inputBytes.Length);

            //step 4. finalize the hash and get the output
            var resultBytes = new byte[engine.GetDigestSize()];

            //step 5. do the finalization and get the hash output
            engine.DoFinal(resultBytes, 0);

            //stop the stopwatch
            sw.Stop();

            var result = Convert.ToHexString(resultBytes);

            return new CryptoResult(result,sw.Elapsed.TotalMilliseconds);
        }
    }
}
