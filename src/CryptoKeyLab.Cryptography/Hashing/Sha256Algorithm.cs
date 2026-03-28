using CryptoKeyLab.Domain.Interfaces.Cryptography;
using CryptoKeyLab.Domain.Models.Cryptography;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace CryptoKeyLab.Cryptography.Hashing
{
    public class Sha256Algorithm : IHashAlgorithm
    {
        public string? Name => "SHA-265";

        public CryptoResult ComputeHash(string input)
        {
            // Start the stopwatch to measure the time taken for hashing
            var sw = Stopwatch.StartNew();

            //Step 2. cresate sha265 instance
            using var sha256 = SHA256.Create();

            //Step 3. Genreate Bytes from input string
            var inputBytes = Encoding.UTF8.GetBytes(input);

            //step 4. compute the hash
            var hashBytes = sha256.ComputeHash(inputBytes);

            //Step 5. Convert the Computes hash into a hexadecimal string
            var hashString = Convert.ToHexString(hashBytes);

            //step 6. Stop the stopwatch and get the elapsed time
            sw.Stop();

            //step 7. Create and return the CryptoResult
            return new CryptoResult(hashString, sw.ElapsedMilliseconds);
        }
    }
}
