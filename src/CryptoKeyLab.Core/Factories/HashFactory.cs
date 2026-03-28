using CryptoKeyLab.Cryptography.Hashing;
using CryptoKeyLab.Domain.Interfaces.Cryptography;
using CryptoKeyLab.Domain.Interfaces.Factories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CryptoKeyLab.Core.Factories
{
    public class HashFactory : IHashFactory
    {
        //Dictonary to hold available algorithms
        private readonly Dictionary<string, Func<IHashAlgorithm>> _alorithmRegistry = new()
        {
            {"SHA-256" , () => new Sha256Algorithm()}
        };
        public IHashAlgorithm Create(string algorithmName)
        {
            //check and return the algorithm if it exists in the registry
            if (_alorithmRegistry.TryGetValue(algorithmName, out var algoFun))
                return algoFun();

            //If the algorithm is not found, throw an exception
            throw new NotSupportedException($"Hash algorithm '{algorithmName}' is not supported yet.");
        }

        //For getting the list of available algorithms in the factory, this can be used for UI dropdowns or API documentation
        public IEnumerable<string> GetAvailableAlgorithms()
        {
            return _alorithmRegistry.Keys;
        }
    }
}
