using CryptoKeyLab.LimitResetWorker.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CryptoKeyLab.LimitResetWorker.Interfaces
{
    public interface IApiKeyMetaData
    {
        Task<IEnumerable<ApiKeyEntity>> GetApiData(bool keyStatus = true,int noOfRowFetch = 100);
    }
}
