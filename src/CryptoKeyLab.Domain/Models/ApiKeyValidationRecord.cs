using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CryptoKeyLab.Domain.Models
{
        // This object tells the Filter exactly what happened during validation
    public record ApiKeyValidationResult
    (
        bool IsValid,
        bool IsReateLimitExceeded,
        string? Message,
        ApiKeyEntity? KeyEntity
    );
}
