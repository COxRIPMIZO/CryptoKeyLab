using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CryptoKeyLab.Domain.Models.Encoding
{
    public class EncodingMetaData
    {
        public int Id { get; set; }
        public string? DisplayName { get; set; }
        public string? ClassName { get; set; }

        // NEW: Added Family for UI Grouping!
        public string? Family { get; set; } = string.Empty;
        public string? FolderName { get; set; } = string.Empty;
        public string? Category { get; set; }
        public bool IsActive { get; set; }
        public int SortOrder { get; set; }
    }
}
