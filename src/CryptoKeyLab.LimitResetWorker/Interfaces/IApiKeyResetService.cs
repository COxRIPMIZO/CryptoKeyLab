using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CryptoKeyLab.LimitResetWorker.Interfaces
{
    public interface IApiKeyResetService
    {
        // 1. Clean name (No time limits mentioned). 
        // 2. Ends in Async. 
        // 3. Takes a list of Guids for maximum Dapper performance.
        Task BulkResetUsageCountsAsync(IEnumerable<Guid> keyIdsToReset);

        // Deactivates keys that have passed their ExpiresAt date
        Task BulkDeactivateExpiredKeysAsync(IEnumerable<Guid> keyIdsToDeactivate);
    }
}
