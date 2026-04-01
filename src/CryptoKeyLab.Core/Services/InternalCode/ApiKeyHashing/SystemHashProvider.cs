using CryptoKeyLab.Cryptography.Hashing.Cryptographic;
using CryptoKeyLab.Domain.Interfaces.Cryptography.Hash;
using CryptoKeyLab.Domain.Interfaces.SystemInternal;
using CryptoKeyLab.Domain.Models.Cryptography;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;

namespace CryptoKeyLab.Core.Services.InternalCode.ApiKeyHashing
{
    public class SystemHashProvider : ISystemHashProvider
    {
        private readonly IHashAlgorithm _hashAlgorithm;

        public SystemHashProvider(IHashAlgorithm algorithm)
        {
            _hashAlgorithm = algorithm ?? throw new ArgumentNullException(nameof(algorithm), "Hash algorithm cannot be null.");
        }
        public string ComputeHash(string input)
        {
            if (string.IsNullOrWhiteSpace(input))
                throw new ArgumentException("Input value cannot be null or empty.", nameof(input));

            //Step 1.Comput hash using the provided algorithm
            var compouteResult = _hashAlgorithm.ComputeHash(new HashOptions(input));

            return compouteResult.OutPut ?? throw new InvalidOperationException("Hash computation failed, output is null.");
        }
    }
}
