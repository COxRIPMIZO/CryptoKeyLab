using CryptoKeyLab.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CryptoKeyLab.Domain.Interfaces
{
    public interface IApiKeyService
    {
        /// <summary>
        /// Generates a cryptographic secure temporary API key valid for 24 hours, with a rate limit of 30 requests per minute. The key is stored securely in the database and can be used for authenticating API requests. The service also provides functionality to validate incoming API keys against the stored keys, ensuring that only valid and active keys are accepted for authentication.
        /// </summary>
        Task<TemporarykeyResponse> GenerateTemporaryKeyAsync();
    }
}
