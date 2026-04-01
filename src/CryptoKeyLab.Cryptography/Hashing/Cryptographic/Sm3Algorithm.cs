using CryptoKeyLab.Domain.Interfaces.Cryptography.Hash;
using CryptoKeyLab.Domain.Models.Cryptography;
using Org.BouncyCastle.Crypto.Digests;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CryptoKeyLab.Cryptography.Hashing.Cryptographic
{
    internal class Sm3Algorithm : IHashAlgorithm
    {
        public string? Name => "SM3 (Chinese Standard)";

        public CryptoResult ComputeHash(HashOptions hashOptions)
        {
            //step 1. input validation
            if (string.IsNullOrWhiteSpace(hashOptions.Input))
            {
                return new CryptoResult(string.Empty,0);
            }

            //step 2. intialize stop watch
            Stopwatch sw = Stopwatch.StartNew();

            //step 3.intiaize the SM3 hash algorithm
            SM3Digest sM3Algo = new();

            //step 4. generate the input byte
            var inputBytes = Encoding.UTF8.GetBytes(hashOptions.Input);

            //step 5. process the inpuot bytes
            sM3Algo.BlockUpdate(inputBytes,0,inputBytes.Length);

            //step 6.calculate the hash (SM3 is always 256 bits / 32 bytes)
            var resultBytes = new byte[sM3Algo.GetDigestSize()];

            //step 7. finalize the hash computation
            sM3Algo.DoFinal(resultBytes,0);

            //step 8. convet into hex string
            var hashResult = Convert.ToHexString(resultBytes);

            sw.Stop();

            return new CryptoResult(hashResult, sw.Elapsed.TotalMilliseconds);
        }
    }
}
