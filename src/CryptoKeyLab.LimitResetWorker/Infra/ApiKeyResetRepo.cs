using CryptoKeyLab.LimitResetWorker.Interfaces;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Data.SqlClient;
using System.Text.Json;
using Dapper;

namespace CryptoKeyLab.LimitResetWorker.Infra
{
    public class ApiKeyResetRepo : IApiKeyResetRepo
    {
        private readonly IConfiguration _configuration;

        public ApiKeyResetRepo(IConfiguration configuration)
        {
            _configuration = configuration ?? throw new ArgumentNullException(nameof(configuration), "Configuration cannot be null.");
        }

        public async Task UpdateUsageCountsToZeroAsync(IEnumerable<Guid> keyIdsToDeactivate)
        {
            IDbConnection connection =  new SqlConnection(_configuration.GetConnectionString("DefaultConnection"));

            // Turn the IEnumerable<Guid> into a JSON array string: '["guid1", "guid2"]'
            string jsonParsingData = JsonSerializer.Serialize(keyIdsToDeactivate);

            //exeute
            await connection.ExecuteAsync("SP_BulkResetUsageCounts", new { jsonApiKeysId  = jsonParsingData },commandType:CommandType.StoredProcedure);
        }

        public async Task SetKeysInactiveAsync(IEnumerable<Guid> keyIdsToReset)
        {
            IDbConnection connection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection"));

            // Turn the IEnumerable<Guid> into a JSON array string: '["guid1", "guid2"]'
            string jsonParsingData = JsonSerializer.Serialize(keyIdsToReset);

            //exeute
            await connection.ExecuteAsync("SP_BulkDeactivateExpiredKeys", new { jsonApiKeysId = jsonParsingData }, commandType: CommandType.StoredProcedure);
        }
    }
}
