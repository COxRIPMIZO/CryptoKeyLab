using CryptoKeyLab.Domain.Interfaces.Cryptography;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CryptoKeyLab.Domain.Interfaces.Factories
{
    public interface IHashFactory
    {
        IHashAlgorithm Create(string algorithmName);
        IEnumerable<string> GetAvailableAlgorithms();
    }
}
