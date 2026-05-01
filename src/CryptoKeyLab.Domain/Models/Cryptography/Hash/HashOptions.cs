using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CryptoKeyLab.Domain.Models.Cryptography.Hash
{
    public record HashOptions
    (
        [Required]
        string Input,
        string? Key = "YourKey",
        string? Salt = "YourSalt",
        int? Iteration = 4
    );
}
