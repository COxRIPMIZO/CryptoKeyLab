using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CryptoKeyLab.Domain.Models.Cryptography
{
    public record CryptoResult
    (
        string? OutPut,
        long TimeTakenMilliSeconds
    );
}
