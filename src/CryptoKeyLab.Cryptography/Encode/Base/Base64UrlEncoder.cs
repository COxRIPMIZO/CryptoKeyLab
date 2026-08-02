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
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.WebUtilities;

namespace CryptoKeyLab.Cryptography.Encode.Base
{
    public class Base64UrlEncoder : IEncodingAlgorithm
    {
        public string? Name => "Base64Url";

        public CryptoResult Decoding(EncodingOptions options)
        {
            if (string.IsNullOrWhiteSpace(options.InputData))
                return new CryptoResult(string.Empty, 0);

            Stopwatch sw = Stopwatch.StartNew();

            try
            {
                byte[] bytes = WebEncoders.Base64UrlDecode(options.InputData);

                string plainText = System.Text.Encoding.UTF8.GetString(bytes);

                sw.Stop();

                return new CryptoResult(plainText, sw.Elapsed.TotalMilliseconds);
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
                string encoded = WebEncoders.Base64UrlEncode(System.Text.Encoding.UTF8.GetBytes(options.InputData));

                sw.Stop();


                return new CryptoResult(encoded, sw.Elapsed.TotalMilliseconds);

            }
            catch (Exception ex)
            {
                sw.Stop();

                return new CryptoResult($"Error: {ex.Message}", sw.Elapsed.TotalMilliseconds);
            }
        }
    }
}
