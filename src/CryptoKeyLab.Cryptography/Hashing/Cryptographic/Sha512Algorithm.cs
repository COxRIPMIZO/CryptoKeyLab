using CryptoKeyLab.Domain.Interfaces.Cryptography.Hash;
using CryptoKeyLab.Domain.Models.Cryptography;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace CryptoKeyLab.Cryptography.Hashing.Cryptographic
{
    public class Sha512Algorithm : IHashAlgorithm
    {
        public string? Name => "SHA-512";

        public CryptoResult ComputeHash(HashOptions hashOptions)
        {
            //start new stopwatch
            var stopwatch = Stopwatch.StartNew();

            //Step 1. create new instance of sha-512
            using var sha512 = SHA512.Create();

            //step 2. Generate bytes of input data
            var inputBytes = Encoding.UTF8.GetBytes(hashOptions.Input);

            //step 3. Compute hash
            var hasBytes = sha512.ComputeHash(inputBytes);

            //step 4.convert hash into hexadecimal string
            var result = Convert.ToHexString(hasBytes);

            //step 5. stop stopwatch and return the result
            stopwatch.Stop();

            return new CryptoResult(result,stopwatch.Elapsed.TotalMilliseconds);
        }
    }
}
