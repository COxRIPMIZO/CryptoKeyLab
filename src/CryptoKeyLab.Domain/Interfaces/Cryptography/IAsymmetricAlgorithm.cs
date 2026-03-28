using CryptoKeyLab.Domain.Models.Cryptography;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CryptoKeyLab.Domain.Interfaces.Cryptography
{
    public interface IAsymmetricAlgorithm
    {
        string? Name { get; }

        //Asymmetric Encryption
        CryptoResult Encrypt(string plainText, string publicKey);
        CryptoResult Decrypt(string cipherText, string privateKey);
    }
}
