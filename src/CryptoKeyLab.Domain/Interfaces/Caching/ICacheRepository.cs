using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CryptoKeyLab.Domain.Interfaces.Caching
{
    public interface ICacheRepository
    {
        ValueTask<T?> GetAsync<T>(string key);
        ValueTask SetAsync<T>(string key, T value, TimeSpan? expiration = null);

        // Delete a specific key
        ValueTask RemoveAsync(string key);

        //Cricual for rete limiting and preventing abuse of the API
        ValueTask<long> IncrementAsync(string key, TimeSpan resetWindow);
    }
}
