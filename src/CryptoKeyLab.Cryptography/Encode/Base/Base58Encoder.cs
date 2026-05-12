using CryptoKeyLab.Domain.Interfaces.Encoding;
using CryptoKeyLab.Domain.Models.Cryptography;
using CryptoKeyLab.Domain.Models.Encoding;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CryptoKeyLab.Cryptography.Encode.Base
{
    public class Base58Encoder : IEncodingAlgorithm
    {
        public string? Name => "Base58";

        public CryptoResult Decoding(EncodingOptions options)
        {

            throw new NotImplementedException();
            //step 1. Validate the input data
            if (string.IsNullOrWhiteSpace(options.InputData))
                return new CryptoResult(string.Empty, 0);

            //var d = Convert.tobas
        }

        public CryptoResult Encoding(EncodingOptions options)
        {
            throw new NotImplementedException();
        }
    }
}
