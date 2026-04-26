using CryptoKeyLab.LimitResetWorker.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CryptoKeyLab.LimitResetWorker.Interfaces
{
    public interface IApiKeyGetService
    {
        Task<IEnumerable<ApiKeyEntity>> GetApiData();
    }
}
