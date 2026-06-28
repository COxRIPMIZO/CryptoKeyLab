using CryptoKeyLab.Domain.Models.Cryptography.Hash;
using CryptoKeyLab.Domain.Models.Encoding;
using CryptoKeyLab.StandAloneWeb.CryptoKeyLab.StandAloneWeb.Domain;
using Microsoft.Extensions.Options;
using System.Net.Http.Json;
using System.Text.Json;

namespace CryptoKeyLab.StandAloneWeb.CryptoKeyLab.StandAloneWeb.Core.Services
{
    public class AlgorithmRegistry : IAlgorithmRegistry
    {
        private readonly HttpClient _httpClient;
        private readonly IConfiguration _configuration;
        public List<HashAlgorithmMetadata> HashAlgorithmMetadata { get; set; } = new();
        public List<EncodingMetaData> EncodingAlgorithmMetadata { get; set; } = new();

        public AlgorithmRegistry(HttpClient httpClient, IConfiguration configuration)
        {
            _httpClient = httpClient;
            _configuration = configuration;
        }

        public async Task InitializeAsync()
        {
            var path = _configuration.GetValue<string>("AlgorithmMetadataPath") ?? throw new InvalidOperationException("Algorithm metadata path is not configured.");

            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };
            // Add this line if Category or any other text property maps to a C# enum:
            options.Converters.Add(new System.Text.Json.Serialization.JsonStringEnumConverter());


            var config = await _httpClient.GetFromJsonAsync<AlgorithmConfig>(path, options);
            if (config == null)
            {
                throw new InvalidOperationException("Failed to load algorithm metadata.");
            }

            HashAlgorithmMetadata = config.HashAlgorithms;
            EncodingAlgorithmMetadata = config.EncodingAlgorithms;
        }
    }
}
