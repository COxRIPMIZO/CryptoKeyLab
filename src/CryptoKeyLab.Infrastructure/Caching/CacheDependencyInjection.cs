using CryptoKeyLab.Domain.Enums;
using CryptoKeyLab.Domain.Interfaces.Caching;
using CryptoKeyLab.Infrastructure.Caching;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CryptoKeyLab.Core.Services.Cache
{
    public static class CacheDependencyInjection
    {
        // Fix 1: Use 'this IServiceCollection' (added the 'I')
        public static IServiceCollection AddCacheServices(this IServiceCollection services,IConfiguration configuration)
        {
            var cacheType = configuration["CacheSettings:Provider"];

            if (cacheType.Equals(CachingProvider.Redis.ToString(), StringComparison.OrdinalIgnoreCase))
            {
                //1.set redis connection multiplexer as singleton, this will manage the connection to the Redis server and allow us to perform operations on the cache
                var redisConnectionString = configuration["CacheSettings:RedisConnectionString"]
                    ?? throw new InvalidOperationException("Redis connection string missing.");

                //2. create a connection multiplexer and register it as a singleton in the DI container, this ensures that the same connection is reused throughout the application, improving performance and resource management
                services.AddSingleton<IConnectionMultiplexer>(ConnectionMultiplexer.Connect(redisConnectionString));
                services.AddSingleton<ICacheRepository, RedisCacheRepository>();
            }
            else
            {
                //1. register the in-memory cache repository as a singleton, this will allow us to use an in-memory cache for caching operations
                services.AddMemoryCache();
                services.AddSingleton<ICacheRepository, InMemoryCacheRepository>();
            }

            return services;
        }
    }
}
