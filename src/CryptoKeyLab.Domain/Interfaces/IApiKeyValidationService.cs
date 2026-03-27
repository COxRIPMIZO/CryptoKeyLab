using CryptoKeyLab.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CryptoKeyLab.Domain.Interfaces
{
    public interface IApiKeyValidationService
    {
        Task<ApiKeyValidationResult> ValidateAndConsumeKeyAsync(string rawApikey);
    }
}
