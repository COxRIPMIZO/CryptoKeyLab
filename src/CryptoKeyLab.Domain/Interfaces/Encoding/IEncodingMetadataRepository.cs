using CryptoKeyLab.Domain.Models.Encoding;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CryptoKeyLab.Domain.Interfaces.Encoding
{
    public interface IEncodingMetadataRepository
    {
        Task<IEnumerable<EncodingMetaData>> GetActiveAlgorithmsAsync();
        Task<EncodingMetaData?> GetAlgorithmByDisplayNameAsync(string encodingAlgoName);
    }
}
