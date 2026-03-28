using CryptoKeyLab.Domain.Models.Cryptography.Hash;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CryptoKeyLab.Domain.Interfaces.Cryptography.Hash
{
    public interface IHashMetadataRepository
    {
        //Complete list of algo
        Task<IEnumerable<HashAlgorithmMetadata>> GetActiveAlgorithmsAsync();

        //Algo Names
        Task<HashAlgorithmMetadata?> GetAlgorithmByDisplayNameAsync(string AlgoDisplayName);
    }
}
