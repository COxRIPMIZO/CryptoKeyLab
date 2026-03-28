using CryptoKeyLab.Domain.Models.Cryptography;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CryptoKeyLab.Domain.Interfaces.Cryptography
{
    public interface ISymmetricAlgorithm
    {
        string? Name { get; }

        //Symmetric Encryption
        CryptoResult Encrypt(string plainText, string key);
        CryptoResult Decrypt(string cipherText, string key);
    }
}
