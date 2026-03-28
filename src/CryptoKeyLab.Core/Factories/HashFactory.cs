using CryptoKeyLab.Cryptography.Hashing;
using CryptoKeyLab.Domain.Interfaces.Cryptography.Hash;
using CryptoKeyLab.Domain.Interfaces.Factories;
using CryptoKeyLab.Domain.Models.Cryptography.Hash;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CryptoKeyLab.Core.Factories
{
    public class HashFactory : IHashFactory
    {
        ////Dictonary to hold available algorithms
        //private readonly Dictionary<string, Func<IHashAlgorithm>> _alorithmRegistry = new()
        //{
        //    {"SHA-256" , () => new Sha256Algorithm()}
        //};
        //public IHashAlgorithm Create(string algorithmName)
        //{
        //    //check and return the algorithm if it exists in the registry
        //    if (_alorithmRegistry.TryGetValue(algorithmName, out var algoFun))
        //        return algoFun();

        //    //If the algorithm is not found, throw an exception
        //    throw new NotSupportedException($"Hash algorithm '{algorithmName}' is not supported yet.");
        //}

        ////For getting the list of available algorithms in the factory, this can be used for UI dropdowns or API documentation
        //public IEnumerable<string> GetAvailableAlgorithms()
        //{
        //    return _alorithmRegistry.Keys;
        //}

        private readonly IHashMetadataRepository _hashMetadataRepository;

        //DI injection of the metadata repository to fetch available algorithms from the database
        public HashFactory(IHashMetadataRepository hashMetaDataRepo) =>
            _hashMetadataRepository = hashMetaDataRepo;

        public async Task<IHashAlgorithm> CreateAsync(string algorithmName)
        {
            //step 1. fetch and create class based on db data
            var metadata = await _hashMetadataRepository.GetAlgorithmByDisplayNameAsync(algorithmName);

            //step 2. if metadata is null, throw an exception
            if (metadata == null)
                throw new NotSupportedException($"Algorithm '{algorithmName}' is not active or does not exist.");

            //step 3. Tell .NET where the class lives (Namespace + Assembly Name)
            var fullClassName = $"CryptoKeyLab.Cryptography.Hashing.{metadata.Category}.{metadata.ClassName}, CryptoKeyLab.Cryptography";

            //step 4.Use reflection to create an instance of the class
            Type? type = Type.GetType(fullClassName);

            if (type == null)
                throw new Exception($"System Error: Class '{metadata.ClassName}' not found in the Cryptography library.");

            //step 5. Cast the created instance to IHashAlgorithm and return it
            return (IHashAlgorithm)Activator.CreateInstance(type)!;
        }

        public async Task<IEnumerable<HashAlgorithmMetadata>> GetAvailableAlgorithmsAsync()
        {
            return await _hashMetadataRepository.GetActiveAlgorithmsAsync();
        }
    }
}
