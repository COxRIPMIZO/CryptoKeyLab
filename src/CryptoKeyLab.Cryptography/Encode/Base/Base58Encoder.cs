using CryptoKeyLab.Domain.Interfaces.Encoding;
using CryptoKeyLab.Domain.Models.Cryptography;
using CryptoKeyLab.Domain.Models.Encoding;
using SimpleBase;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CryptoKeyLab.Cryptography.Encode.Base
{
    public class Base58Encoder : IEncodingAlgorithm
    {
        public string? Name => "Base58 (Bitcoin)";

        Stopwatch sw = Stopwatch.StartNew();
        public CryptoResult Decoding(EncodingOptions options)
        {
            byte[] bytes = Base58.Bitcoin.Decode(options.InputData);

            // 2. Convert those bytes back into the original plain text
            var var_text = System.Text.Encoding.UTF8.GetString(bytes);

            sw.Stop();

            return new CryptoResult(var_text, sw.Elapsed.TotalMilliseconds);
        }

        public CryptoResult Encoding(EncodingOptions options)
        {
            if (string.IsNullOrWhiteSpace(options.InputData))
                return new CryptoResult(string.Empty, 0);

            Stopwatch sw = Stopwatch.StartNew();

            try
            {
               // 1. Convert plain text (e.g., "test") into a byte array
                      byte[] bytes = System.Text.Encoding.UTF8.GetBytes(options.InputData);
                // 2. Encode the byte array into a Base58 string
                var var_text = Base58.Bitcoin.Encode(bytes);

                sw.Stop();

                return new CryptoResult(var_text, sw.Elapsed.TotalMilliseconds);
            }
            catch
            {
                sw.Stop();

                return new CryptoResult("Error: Invalid Base58 input string.",sw.Elapsed.TotalMilliseconds);
            }
        }
    }
}
