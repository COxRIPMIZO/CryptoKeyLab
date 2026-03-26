using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CryptoKeyLab.Domain.Models
{
    // This class represents an API key entity with a unique identifier.
    // It can be extended with additional properties such as the key value, expiration date, and associated user information.
    //it is a simple model that can be used to manage API keys in the application, allowing for secure access control and authentication.
    //store in a database or in-memory collection, and can be used to validate incoming API requests by checking the provided API key against the stored keys.
    public class ApiKeyEntity
    {
        public Guid Id { get; set; } = Guid.NewGuid();

        public string keyPrefix { get; set; } = string.Empty;
        public string KeyHash { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime ExpiresAt { get; set; }

        public bool IsActive => DateTime.UtcNow < ExpiresAt;
        public int RateLimitPerMinute { get; set; } = 30; // Default rate limit
    }

    // 2. The DTO (What the API actually sends back to the user)
    // We use a 'record' here because it's an industry standard for immutable data transfers.
    public record TemporarykeyResponse
    (
        string ApiKey,
        DateTime ExpireAt,
        string Message,
        int RateLimitPerMinute
    );
}
