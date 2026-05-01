using CryptoKeyLab.Domain.Models.Cryptography;
using CryptoKeyLab.Domain.Models.Cryptography.Hash;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CryptoKeyLab.Domain.Interfaces.Cryptography.Hash
{
    public interface IHashAlgorithm
    {
        string? Name { get; }

        /// Results
        /// change for multiple input parameters
        //CryptoResult ComputeHash(string input);

        CryptoResult ComputeHash(HashOptions hashOptions);
    }
}
