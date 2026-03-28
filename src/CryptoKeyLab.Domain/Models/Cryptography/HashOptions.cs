using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CryptoKeyLab.Domain.Models.Cryptography
{
    public record HashOptions
    (
        string Input,
        string? Key = null,
        string? Salt = null,
        int? Iteration = null
    );
}
