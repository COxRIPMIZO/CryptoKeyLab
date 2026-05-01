using CryptoKeyLab.Domain.Interfaces.Cryptography.Hash;
using CryptoKeyLab.Domain.Models.Cryptography;
using CryptoKeyLab.Domain.Models.Cryptography.Hash;
using Org.BouncyCastle.Crypto.Digests;
using Org.BouncyCastle.Utilities;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CryptoKeyLab.Cryptography.Hashing.Deprecated
{
    public class Sha1Algorithm : IHashAlgorithm
    {
        public string? Name => "SHA-1";

        public CryptoResult ComputeHash(HashOptions hashOptions)
        {
            //check inpuot validation
            if (string.IsNullOrWhiteSpace(hashOptions.Input))
            {
                throw new ArgumentException("Input cannot be null or empty.", nameof(hashOptions.Input));
            }

            //step 1 .initialize the stop watch
            Stopwatch sw = Stopwatch.StartNew();

            //step 2.initilize the SHA1 algorithm
            var sha1Algo = new Sha1Digest();

            //step 3. get input bytes
            byte[] inputBytes = Encoding.UTF8.GetBytes(hashOptions.Input);

            //step 4. updated digetst block
            sha1Algo.BlockUpdate(inputBytes,0,inputBytes.Length);

            //step 5.compute the digest bytes
            byte[] resultBytes = new byte[sha1Algo.GetDigestSize()];

            sha1Algo.DoFinal(resultBytes, 0);

            //step convert into string
            string res = Convert.ToHexString(resultBytes);

            sw.Stop();

            return new CryptoResult(res,sw.Elapsed.TotalMilliseconds);
        }
    }
}
