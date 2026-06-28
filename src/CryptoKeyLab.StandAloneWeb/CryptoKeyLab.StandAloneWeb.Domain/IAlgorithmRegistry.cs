using CryptoKeyLab.Domain.Models.Cryptography.Hash;
using CryptoKeyLab.Domain.Models.Encoding;

namespace CryptoKeyLab.StandAloneWeb.CryptoKeyLab.StandAloneWeb.Domain
{
    public interface IAlgorithmRegistry
    {
        List<HashAlgorithmMetadata> HashAlgorithmMetadata { get; set; }
        List<EncodingMetaData> EncodingAlgorithmMetadata { get; set; }
        Task InitializeAsync();
    }
}
