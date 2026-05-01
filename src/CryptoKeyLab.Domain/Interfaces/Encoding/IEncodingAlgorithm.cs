using CryptoKeyLab.Domain.Models.Cryptography;
using CryptoKeyLab.Domain.Models.Encoding;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CryptoKeyLab.Domain.Interfaces.Encoding
{
    public interface IEncodingAlgorithm
    {
        string? Name { get; }

        CryptoResult Encoding(EncodingOptions options);
        CryptoResult Decoding(EncodingOptions options);
    }
}
