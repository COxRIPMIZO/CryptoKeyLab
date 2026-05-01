using CryptoKeyLab.Domain.Interfaces.Encoding;
using CryptoKeyLab.Domain.Models.Encoding;
using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace CryptoKeyLab.Infrastructure.Repositories.Encoding
{
    public class EncodingMetadataRepository : IEncodingMetadataRepository
    {
        private readonly string? _connectionString;

        public EncodingMetadataRepository (IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string missing.");
        }

        public async Task<IEnumerable<EncodingMetaData>> GetActiveAlgorithmsAsync()
        {
            IDbConnection connection = new SqlConnection(_connectionString);

            return await connection.QueryAsync<EncodingMetaData>("SP_GetActiveEncodingAlgorithms",commandType : CommandType.StoredProcedure);
        }

        public async Task<EncodingMetaData?> GetAlgorithmByDisplayNameAsync(string encodingAlgoName)
        {
            IDbConnection connection = new SqlConnection(_connectionString);

            return await connection.QueryFirstOrDefaultAsync<EncodingMetaData>("SP_GetEncodingAlgoByDisplayName", new { DisplayName = encodingAlgoName }, commandType: CommandType.StoredProcedure);
        }
    }
}
