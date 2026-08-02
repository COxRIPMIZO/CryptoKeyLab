using CryptoKeyLab.Domain.Interfaces.Caching;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace CryptoKeyLab.Infrastructure.Caching
{
    public class RedisCacheRepository : ICacheRepository
    {
        private readonly IDatabase _db;

        public RedisCacheRepository(IConnectionMultiplexer redisDb)
        {
            _db = redisDb.GetDatabase();
        }

        public async ValueTask<T?> GetAsync<T>(string key)
        {
            var value = await _db.StringGetAsync(key);

            if (value.IsNullOrEmpty)
                return default;

            return JsonSerializer.Deserialize<T>(value!);
        }

        public async ValueTask<long> IncrementAsync(string key, TimeSpan resetWindow)
        {
            long count = await _db.StringIncrementAsync(key);

            // If this is the FIRST time we incremented (count == 1), set the expiry timer!
            if(count == 1)
                await _db.KeyExpireAsync(key, resetWindow);

            return count;
        }

        public async ValueTask RemoveAsync(string key)
        {
            await _db.KeyDeleteAsync(key);
        }

        public async ValueTask SetAsync<T>(string key, T value, TimeSpan? expiration = null)
        {
            // 1. Serialization can be optimized with options if needed
            var jsonValue = JsonSerializer.Serialize(value);

            // Explicitly cast the inner TimeSpan to Expiration
            var redisExpiry = expiration.HasValue
                ? (Expiration)expiration.Value
                : Expiration.Default;

            // 2. Pass the nullable TimeSpan directly; StackExchange.Redis handles null as 'no expiry'
            await _db.StringSetAsync(key, jsonValue, redisExpiry);
        }
    }
}
