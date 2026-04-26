
using CryptoKeyLab.LimitResetWorker.Interfaces;
using CryptoKeyLab.LimitResetWorker.Models;
using Dapper;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CryptoKeyLab.LimitResetWorker.Infra
{
    public class ApiKeyMetaData : IApiKeyMetaData
    {
        private readonly IConfiguration _configuration;

        public ApiKeyMetaData(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task<IEnumerable<ApiKeyEntity>> GetApiData(bool keyStatus, int noOfRowFetch)
        {
            IDbConnection connection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection"));

            return await connection.QueryAsync<ApiKeyEntity>("SP_GetApiKeyData",new { IsActive = keyStatus, RowToFetch = noOfRowFetch },commandType: CommandType.StoredProcedure);
        }

    }
}
