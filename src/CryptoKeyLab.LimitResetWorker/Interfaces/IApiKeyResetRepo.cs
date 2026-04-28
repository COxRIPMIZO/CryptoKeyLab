using CryptoKeyLab.LimitResetWorker.Infra;
using CryptoKeyLab.LimitResetWorker.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CryptoKeyLab.LimitResetWorker.Interfaces
{
    public interface IApiKeyResetRepo
    {
        // 1. Clean name (No time limits mentioned). 
        // 2. Ends in Async. 
        // 3. Takes a list of Guids for maximum Dapper performance.
        Task UpdateUsageCountsToZeroAsync(IEnumerable<Guid> keyIdsToReset);

        // Deactivates keys that have passed their ExpiresAt date
        Task SetKeysInactiveAsync(IEnumerable<Guid> keyIdsToDeactivate);
    }
}
