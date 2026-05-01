using CryptoKeyLab.Domain.Interfaces.Cryptography.Hash;
using CryptoKeyLab.Domain.Models.Cryptography;
using CryptoKeyLab.Domain.Models.Cryptography.Hash;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace CryptoKeyLab.Cryptography.Hashing.Cryptographic.Sha2
{
    public class Sha384Algorithm : IHashAlgorithm
    {
        public string? Name => "SHA-384";

        public CryptoResult ComputeHash(HashOptions hashOptions)
        {
            //start new stopwatch
            var stopwatch = Stopwatch.StartNew();

            //Step 1. create new instance of sha384
            using var sha384 = SHA384.Create();

            //step 2. Generate bytes of input data
            var inputBytes = Encoding.UTF8.GetBytes(hashOptions.Input);

            //step 3. Compute hash
            var hasBytes = sha384.ComputeHash(inputBytes);

            //step 4.convert hash into hexadecimal string
            var result = Convert.ToHexString(hasBytes);

            //step 5. stop stopwatch and return the result
            stopwatch.Stop();

            return new CryptoResult(result, stopwatch.Elapsed.TotalMilliseconds);
        }
    }
}
