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

namespace CryptoKeyLab.Cryptography.Hashing.Cryptographic.Sha3
{
    public class Sha3_256Algorithm : IHashAlgorithm
    {
        public string? Name => "SHA3_256";

        public CryptoResult ComputeHash(HashOptions hashOptions)
        {
            //step 1. create an instance of stop watch 
            Stopwatch sw = Stopwatch.StartNew();

            //step 2. create instance of sha3 algorithm
            using var sha3 = SHA3_256.Create();

            //step 3.generate input bytes from the input string
            var inputBytes = Encoding.UTF8.GetBytes(hashOptions.Input);

            //step 4. compute the hash
            var hashBytes = sha3.ComputeHash(inputBytes);

            //step 5. convert the hash bytes to a hexadecimal string
            var hashString = Convert.ToHexString(hashBytes);

            //step 5. stop the stop watch
            sw.Stop();

            //step 6. return the result
            return new CryptoResult
            (
                OutPut : hashString,
                TimeTakenMilliSeconds : sw.Elapsed.TotalMilliseconds
            );
        }
    }
}
