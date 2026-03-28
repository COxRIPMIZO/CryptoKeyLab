using CryptoKeyLab.Domain.Models.Cryptography;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CryptoKeyLab.Domain.Interfaces.Cryptography
{
    public interface IRandomGenerator
    {
        string? Name { get; }

        /// Results
        CryptoResult GeneratePassword(int length,bool isUseSpecialChars);
    }
}
