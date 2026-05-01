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

namespace CryptoKeyLab.Cryptography.Hashing.Cryptographic
{
    public class WhirlpoolAlgorithm : IHashAlgorithm
    {
        public string? Name => "Whirlpool";

        public CryptoResult ComputeHash(HashOptions hashOptions)
        {
            // Step 1. Validate input
            if(string.IsNullOrWhiteSpace(hashOptions.Input))
                return new CryptoResult(string.Empty, 0);

            // Step 2. Start timer
            Stopwatch sw = Stopwatch.StartNew();

            //step 3. generate input bytes
            var inputbytes = Encoding.UTF8.GetBytes(hashOptions.Input);

            //step 2. intialize the whirlpool algorithm
            var whirlpooldigest = new WhirlpoolDigest();

            //step 3. process the data
            whirlpooldigest.BlockUpdate(inputbytes,0,inputbytes.Length);

            // 4. Calculate final hash (Whirlpool is always 512 bits / 64 bytes)
            var resultBytes = new byte[whirlpooldigest.GetDigestSize()];

            whirlpooldigest.DoFinal(resultBytes, 0);

            //step 5.convert the rsult 
            var result = Convert.ToHexString(resultBytes);

            //step 6. stop timer and return result
            sw.Stop();

            return new CryptoResult(result, sw.Elapsed.TotalMilliseconds);
        }
    }
}
