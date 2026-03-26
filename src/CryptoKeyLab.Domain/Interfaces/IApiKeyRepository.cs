using CryptoKeyLab.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CryptoKeyLab.Domain.Interfaces
{
    public interface IApiKeyRepository
    {
        Task<ApiKeyEntity> GetByKeyHashAsync(string strKeyHash);

        Task IncrementUsageAsync(Guid keyId);

        Task CreatekeyAsync(ApiKeyEntity entity);
    }
}
