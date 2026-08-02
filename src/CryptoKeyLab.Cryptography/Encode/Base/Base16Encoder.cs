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
    public class Base16Encoder : IEncodingAlgorithm
    {
        public string? Name => "Base16 (Hex)";

        public CryptoResult Decoding(EncodingOptions options)
        {
            if (string.IsNullOrWhiteSpace(options.InputData))
                return new CryptoResult(string.Empty, 0);

            Stopwatch sw = Stopwatch.StartNew();

            try
            {
                byte[] bytes = Convert.FromHexString(options.InputData);

                // 2. Convert the byte array back into the original plain text (e.g., "test")
                var var_text = System.Text.Encoding.UTF8.GetString(bytes);

                sw.Stop();


                return new CryptoResult(var_text, sw.Elapsed.TotalMilliseconds);
            }
            catch
            {
                sw.Stop();

                return new CryptoResult("Error: Invalid Base32 input string.", sw.Elapsed.TotalMilliseconds);
            }
        }

        public CryptoResult Encoding(EncodingOptions options)
        {
            if (string.IsNullOrWhiteSpace(options.InputData))
                return new CryptoResult(string.Empty, 0);

            Stopwatch sw = Stopwatch.StartNew();

            try
            {
                //byte[] bytes = Convert.FromHexString(options.InputData);

                //var var_text = System.Text.Encoding.UTF8.GetString(bytes);

                //sw.Stop();

                //return new CryptoResult(var_text, sw.Elapsed.TotalMilliseconds);

                // 1. Convert plain text (e.g., "test") into a byte array
                byte[] bytes = System.Text.Encoding.UTF8.GetBytes(options.InputData);

                // 2. Convert that byte array into a Base-16 Hex string (Output: "74657374")
                var var_text = Convert.ToHexString(bytes);

                sw.Stop();

                return new CryptoResult(var_text, sw.Elapsed.TotalMilliseconds);
            }
            catch (Exception ex)
            {
                sw.Stop();

                return new CryptoResult($"Error: {ex.Message}", sw.Elapsed.TotalMilliseconds);
            }
        }
    }
}
