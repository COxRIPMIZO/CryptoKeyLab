using CryptoKeyLab.Domain.Interfaces.Cryptography.Hash;
using CryptoKeyLab.Domain.Models.Cryptography;
using CryptoKeyLab.Domain.Models.Cryptography.Hash;
using Org.BouncyCastle.Crypto.Digests;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CryptoKeyLab.Cryptography.Hashing.Cryptographic
{
    public class StreebogAlgorithm : IHashAlgorithm
    {
        public string? Name => "Streebog (Russian Standard)";

        public CryptoResult ComputeHash(HashOptions hashOptions)
        {
            //check input validation
            if (string.IsNullOrWhiteSpace(hashOptions.Input))
                throw new ArgumentException("Input cannot be null or empty.", nameof(hashOptions.Input));

            Stopwatch sw = Stopwatch.StartNew();

            //step 1: Convert the input string to bytes
            var inputbytes = Encoding.UTF8.GetBytes(hashOptions.Input);

            //step 2: Initialize the Streebog state and parameters
            var streebogObj = new Gost3411_2012_512Digest();

            //step 3: Compute the hash using Streebog algorithm
            streebogObj.BlockUpdate(inputbytes, 0, inputbytes.Length);

            //step 4: Finalize the hash computation and get the result
            var resultBytes = new byte[streebogObj.GetDigestSize()];

            //finalize the hash computation and get the result
            streebogObj.DoFinal(resultBytes, 0);

            var result = Convert.ToHexString(resultBytes);

            sw.Stop();

            return new CryptoResult
            (
                OutPut: result,
                TimeTakenMilliSeconds: sw.Elapsed.TotalMilliseconds
            );
        }
    }
}
