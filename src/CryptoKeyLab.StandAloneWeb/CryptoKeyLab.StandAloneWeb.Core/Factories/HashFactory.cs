using CryptoKeyLab.Cryptography.Hashing;
using CryptoKeyLab.Domain.Interfaces.Cryptography.Hash;
using CryptoKeyLab.Domain.Interfaces.Factories;
using CryptoKeyLab.Domain.Models.Cryptography.Hash;
using CryptoKeyLab.StandAloneWeb.CryptoKeyLab.StandAloneWeb.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CryptoKeyLab.StandAloneWeb.Core.Factories
{
    public class HashFactory : IHashFactory
    {
        private readonly IHashMetadataRepository _hashMetadataRepository;
        private readonly IAlgorithmRegistry _algorithmRegistry;

        //DI injection of the metadata repository to fetch available algorithms from the database
        public HashFactory(IAlgorithmRegistry algorithmRegistry) =>
            _algorithmRegistry = algorithmRegistry;

        public Task<IHashAlgorithm> CreateAsync(string algorithmName)
        {
            //step 1. fetch and create class based on db data
            //var metadata = await _hashMetadataRepository.GetAlgorithmByDisplayNameAsync(algorithmName);
            var metadata = _algorithmRegistry.HashAlgorithmMetadata.FirstOrDefault(p => p.DisplayName == algorithmName);

            //step 2. if metadata is null, throw an exception
            if (metadata == null)
                throw new NotSupportedException($"Algorithm '{algorithmName}' is not active or does not exist.");

            //step 3. Tell .NET where the class lives (Namespace + Assembly Name)
            //var fullClassName = $"CryptoKeyLab.Cryptography.Hashing.{metadata.Category}.{metadata.ClassName}, CryptoKeyLab.Cryptography";

            //===================================================
            //Fix : added family name as well in path of class library as we classified the application based on family as well
            //added : 04-04-2026
            //===================================================
            
            var fullClassName = string.IsNullOrWhiteSpace(metadata.FolderName) 
                                ?  $"CryptoKeyLab.Cryptography.Hashing.{metadata.Category}.{metadata.ClassName}, CryptoKeyLab.Cryptography"
                                : $"CryptoKeyLab.Cryptography.Hashing.{metadata.Category}.{metadata.FolderName}.{metadata.ClassName}, CryptoKeyLab.Cryptography";

            //step 4.Use reflection to create an instance of the class
            Type? type = Type.GetType(fullClassName);

            if (type == null)
                throw new Exception($"System Error: Class '{metadata.ClassName}' not found in the Cryptography library.");

            //step 5. Cast the created instance to IHashAlgorithm and return it
            return (Task<IHashAlgorithm>)Activator.CreateInstance(type)!;
        }

        public async Task<IEnumerable<HashAlgorithmMetadata>> GetAvailableAlgorithmsAsync()
        {
            return await _hashMetadataRepository.GetActiveAlgorithmsAsync();
        }
    }
}
