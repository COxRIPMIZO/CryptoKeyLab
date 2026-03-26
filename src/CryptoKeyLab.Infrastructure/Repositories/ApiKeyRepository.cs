using CryptoKeyLab.Domain.Interfaces;
using CryptoKeyLab.Domain.Models;
using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace CryptoKeyLab.Infrastructure.Repositories
{
    public class ApiKeyRepository : IApiKeyRepository
    {
        //connection string 
        private readonly string _connectionString;

        // Dependency Injection: We ask .NET for the IConfiguration to read appsettings.json
        public ApiKeyRepository(IConfiguration config)
        {
            _connectionString = config.GetConnectionString("DefaultConnection")
                ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
        }
        public async Task CreatekeyAsync(ApiKeyEntity entity)
        {
            using IDbConnection db = new SqlConnection(_connectionString);

            // Pass exactly what the SP expects!
            var parameters = new
            {
                Id = entity.Id,
                keyPrefix = entity.keyPrefix,
                KeyHash = entity.KeyHash,
                CreatedAt = entity.CreatedAt,
                ExpiresAt = entity.ExpiresAt, // Maps the C# 'ExpiresAt' to your SP's '@ExpireAt'
                RateLimitPerMinute = entity.RateLimitPerMinute
            };

            //execute
            await db.ExecuteAsync("SP_CreateApiKey", parameters, commandType : CommandType.StoredProcedure);
        }

        public async Task<ApiKeyEntity> GetByKeyHashAsync(string strKeyHash)
        {
            using IDbConnection dbConnection = new SqlConnection(_connectionString);

            return await dbConnection.QueryFirstOrDefaultAsync<ApiKeyEntity>("SP_GetValidApiKey", new { KeyHash = strKeyHash},commandType: CommandType.StoredProcedure);
        }

        public async Task IncrementUsageAsync(Guid keyId)
        {
            using IDbConnection db = new SqlConnection(_connectionString);

            await db.ExecuteAsync("SP_IncrementApiKeyUsage", new { KeyId = keyId },commandType:CommandType.StoredProcedure);
        }
    }
}
