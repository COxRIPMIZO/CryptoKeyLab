using CryptoKeyLab.LimitResetWorker.Interfaces;
using CryptoKeyLab.LimitResetWorker.Models;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CryptoKeyLab.LimitResetWorker.Services
{
    internal class ApiKeyGetService : IApiKeyGetService
    {
        private readonly IOptionsMonitor<ResetWorkerSettings> _optionMonitor;
        private readonly IApiKeyMetaData _apiKeyMetaData;
        public ApiKeyGetService(IOptionsMonitor<ResetWorkerSettings> optionsMonitor,IApiKeyMetaData apiKeyMetaData) 
        {
            _optionMonitor = optionsMonitor;
            _apiKeyMetaData = apiKeyMetaData;
        }
        public async Task<IEnumerable<ApiKeyEntity>> GetApiData()
        {
            return await _apiKeyMetaData.GetApiData(_optionMonitor.CurrentValue.KeyStatus,_optionMonitor.CurrentValue.NoOfDataToFetch);
        }
    }
}
