using CryptoKeyLab.Domain.Interfaces.Cryptography.Hash;
using CryptoKeyLab.Domain.Models.Cryptography.Hash;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CryptoKeyLab.Domain.Interfaces.Factories
{
    public interface IHashFactory
    {
        //IHashAlgorithm Create(string algorithmName);
        //IEnumerable<string> GetAvailableAlgorithms();

        Task<IEnumerable<HashAlgorithmMetadata>> GetAvailableAlgorithmsAsync();

        Task<IHashAlgorithm> CreateAsync(string algorithmName);
    }
}
