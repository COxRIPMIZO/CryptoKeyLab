using CryptoKeyLab.Domain.Interfaces.Encoding;
using CryptoKeyLab.Domain.Models.Encoding;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CryptoKeyLab.Domain.Interfaces.Factories
{
    public interface IEncodingFactory
    {
        Task<IEnumerable<EncodingMetaData>> GetAvailableAlgorithmsAsync();

        //creat encoding class instance based on name provided
        Task<IEncodingAlgorithm> CreateAsync(string algorithm);
    }
}
