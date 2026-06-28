using CryptoKeyLab.Domain.Models.Cryptography.Hash;
using CryptoKeyLab.Domain.Models.Encoding;
using System.Text.Json.Serialization;

namespace CryptoKeyLab.StandAloneWeb.CryptoKeyLab.StandAloneWeb.Domain
{
    public class AlgorithmConfig
    {
        [JsonPropertyName("HashAlgorithms")]
        public List<HashAlgorithmMetadata> HashAlgorithms { get; set; } = new();

        [JsonPropertyName("EncodingAlgorithms")]
        public List<EncodingMetaData> EncodingAlgorithms { get; set; } = new();
    }
}
