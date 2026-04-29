using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CryptoKeyLab.LimitResetWorker.Models
{
    public class ApiKeyEntity
    {
        public Guid Id { get; set; } = Guid.NewGuid();

        public DateTime ExpiresAt { get; set; }

        public int TotalUsageCount { get; set; } = 0; // Track total usage count for analytics or rate limiting purposes

        public bool IsActive => DateTime.UtcNow < ExpiresAt;
        public int RateLimitPerMinute { get; set; } = 30; // Default rate limit

        public DateTime LastUsageReset { get; set; }
    }
}
