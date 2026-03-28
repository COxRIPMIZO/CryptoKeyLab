using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CryptoKeyLab.Domain.Models.Cryptography.Hash
{
    public class HashAlgorithmMetadata
    {
        public int Id { get; set; }
        public string? DisplayName { get; set; }
        public string? ClassName { get; set; }
        public string? Category { get; set; }
        public bool RequiresKey { get; set; }
        public bool RequiresSalt { get; set; }
        public bool RequiresIterations { get; set; }
        public bool IsActive { get; set; }
        public bool IsSecure { get; set; }
        public int SortOrder { get; set; }

    }
}
