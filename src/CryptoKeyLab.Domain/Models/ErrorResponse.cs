using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CryptoKeyLab.Domain.Models
{
        //immutability!
        public record ErrorResponse
        (
            int StatusCode,
            string? Title,
            string? Detail = null
        );
}
