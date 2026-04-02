using CryptoKeyLab.Domain.Interfaces.Cryptography.Hash;
using CryptoKeyLab.Domain.Models.Cryptography;
using Org.BouncyCastle.Crypto.Digests;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CryptoKeyLab.Cryptography.Hashing.Deprecated
{
    public class Md5Algorithm : IHashAlgorithm
    {
        public string? Name => "MD5";

        public CryptoResult ComputeHash(HashOptions hashOptions)
        {
            //check input
            if (string.IsNullOrWhiteSpace(hashOptions.Input))
                throw new ArgumentException("Input cannot be null or empty.", nameof(hashOptions.Input));

            Stopwatch sw = Stopwatch.StartNew();

            //
            var mdAlgo = new MD5Digest();

            var inputBytes = Encoding.UTF8.GetBytes(hashOptions.Input);

            mdAlgo.BlockUpdate(inputBytes,0,inputBytes.Length);

            var resultBytes = new byte[mdAlgo.GetDigestSize()];

            mdAlgo.DoFinal(resultBytes, 0);

            sw.Stop();
            
            string resultHex = Convert.ToHexString(resultBytes);

            return new CryptoResult(resultHex,sw.Elapsed.TotalMilliseconds);
            
        }
    }
}
