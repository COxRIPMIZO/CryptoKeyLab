using CryptoKeyLab.Domain.Interfaces.Cryptography.Hash;
using CryptoKeyLab.Domain.Models.Cryptography.Hash;
using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;

namespace CryptoKeyLab.Infrastructure.Repositories.Cryptography.HashMetaData
{
    public class HashMetadataRepository : IHashMetadataRepository
    {
        private readonly string? _connectionString;

        //DI injection of configuration to get connection string
        public HashMetadataRepository(IConfiguration config) =>
            _connectionString = config.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string missing.");
        public async Task<IEnumerable<HashAlgorithmMetadata>> GetActiveAlgorithmsAsync()
        {
            using IDbConnection con = new SqlConnection(_connectionString);

            return await con.QueryAsync<HashAlgorithmMetadata>("SP_GetActiveHashAlgorithms",null,commandType: CommandType.StoredProcedure);
        }

        public async Task<HashAlgorithmMetadata?> GetAlgorithmByDisplayNameAsync(string AlgoDisplayName)
        {
            using IDbConnection db = new SqlConnection(_connectionString);

            return await db.QueryFirstOrDefaultAsync<HashAlgorithmMetadata>("SP_GetHashAlgoByDisplayName", new { DisplayName = AlgoDisplayName },commandType:CommandType.StoredProcedure);
        }
    }
}
