using CryptoKeyLab.Domain.Interfaces.Encoding;
using CryptoKeyLab.Domain.Interfaces.Factories;
using CryptoKeyLab.Domain.Models.Encoding;
using CryptoKeyLab.StandAloneWeb.CryptoKeyLab.StandAloneWeb.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CryptoKeyLab.StandAloneWeb.Core.Factories
{
    public class EncodingFactory : IEncodingFactory
    {
        private readonly IEncodingMetadataRepository _metadataRepository;
        private readonly IAlgorithmRegistry _algorithmRegistry;
        public EncodingFactory(IAlgorithmRegistry algorithmRegistry)
        {
            _algorithmRegistry = algorithmRegistry;
        }

        public async Task<IEncodingAlgorithm> CreateAsync(string algorithm)
        {
            //step 1.check existence of algorithm
            //var algoMetaData = await _metadataRepository.GetAlgorithmByDisplayNameAsync(algorithm);
            var algoMetaData = _algorithmRegistry.EncodingAlgorithmMetadata.FirstOrDefault(p => p.DisplayName == algorithm);

            //step 2.check and return message
            if (algoMetaData == null)
                throw new NotSupportedException($"Algorithm '{algorithm}' is not active or does not exist.");

            //step 3.Tell .NET where the class lives (Namespace + Assembly Name)
            string className = string.IsNullOrWhiteSpace(algoMetaData.FolderName) ? $"CryptoKeyLab.Cryptography.{algoMetaData.Category}.{algoMetaData.ClassName},CryptoKeyLab.Cryptography" : $"CryptoKeyLab.Cryptography.{algoMetaData.Category}.{algoMetaData.FolderName}.{algoMetaData.ClassName},CryptoKeyLab.Cryptography";
            
            //step 4.get the type or assembly of classname
            var classInstance = Type.GetType(className);

            if(classInstance == null)
                throw new Exception($"System Error: Class '{algoMetaData.ClassName}' not found in the Cryptography library.");

            //step 5.create an instance of classname using reflection'
            return (IEncodingAlgorithm)Activator.CreateInstance(classInstance)!;
        }

        public async Task<IEnumerable<EncodingMetaData>> GetAvailableAlgorithmsAsync()
        {
            return await _metadataRepository.GetActiveAlgorithmsAsync();
        }
    }
}
