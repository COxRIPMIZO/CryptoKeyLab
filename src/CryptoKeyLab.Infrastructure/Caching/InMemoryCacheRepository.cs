using CryptoKeyLab.Domain.Interfaces.Caching;
using System;
using System.Buffers;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Caching.Memory;
using System.Collections.Concurrent;

namespace CryptoKeyLab.Infrastructure.Caching
{
    public class InMemoryCacheRepository : ICacheRepository
    {
        //memory cache is a thread-safe cache implementation provided by Microsoft.Extensions.Caching.Memory. It allows you to store and retrieve objects in memory with support for expiration and eviction policies.
        private readonly IMemoryCache _memoryCache;
        private readonly ConcurrentDictionary<string, object> _locks = new();

        public ValueTask<T?> GetAsync<T>(string key)
        {
            _memoryCache.TryGetValue(key, out T? value);
            return ValueTask.FromResult(value);
        }

        // Thread-Safe Atomic Increment for RAM
        public ValueTask<long> IncrementAsync(string key, TimeSpan resetWindow)
        {
            var lockObj = _locks.GetOrAdd(key, _ => new object());

            //add increament and decreament logic here
            lock (lockObj)
            {
                var currentCount = _memoryCache.GetOrCreate(key, entry => 
                {
                    entry.AbsoluteExpirationRelativeToNow = resetWindow; // Set expiration for the counter
                    return 0L; // Initialize counter to 0
                });

                currentCount += 1; // Increment the counter

                //set the new cache
                _memoryCache.Set(key, currentCount, resetWindow);

                return ValueTask.FromResult(currentCount);
            }
        }

        public ValueTask RemoveAsync(string key)
        {
            _memoryCache.Remove(key);
            return ValueTask.CompletedTask;
        }

        public ValueTask SetAsync<T>(string key, T value, TimeSpan? expiration = null)
        {
            var options = new MemoryCacheEntryOptions();

            //check expiration
            if (expiration.HasValue)
                options.SetAbsoluteExpiration(expiration.Value);

            //set cache
            _memoryCache.Set(key, value, options);
            return ValueTask.CompletedTask;
        }
    }
}
