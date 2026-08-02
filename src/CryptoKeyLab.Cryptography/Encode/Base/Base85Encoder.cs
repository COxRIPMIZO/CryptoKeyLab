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
    public class Base85Encoder : IEncodingAlgorithm
    {
        public string? Name => "Base85 (Ascii85)";

        public CryptoResult Decoding(EncodingOptions options)
        {
            //step 1.check the parmaters
            if (string.IsNullOrWhiteSpace(options.InputData))
                return new CryptoResult(string.Empty, 0);

            //step 2. create stop watch 
            Stopwatch sw = Stopwatch.StartNew();

            try
            {
                //step 3.get bytes of inputs
                Span<byte> bytes = Convert.FromBase64String(options.InputData);

                //step 4. convert it into base64
                var plainText = System.Text.Encoding.UTF8.GetString(bytes);

                sw.Stop();

                return new CryptoResult(plainText, sw.Elapsed.TotalMilliseconds);
            }
            catch (Exception)
            {
                sw.Stop();
                return new CryptoResult("Error: Invalid Base64 input string.", sw.Elapsed.TotalMilliseconds);
            }
        }

        public CryptoResult Encoding(EncodingOptions options)
        {
            if (string.IsNullOrWhiteSpace(options.InputData))
                return new CryptoResult(string.Empty, 0);

            Stopwatch sw = Stopwatch.StartNew();

            try
            {
                string encoded=""; //Ascii85.Encode(System.Text.Encoding.UTF8.GetBytes(options.InputData));

                sw.Stop();

                return new CryptoResult(encoded,sw.Elapsed.TotalMilliseconds);
            }
            catch (Exception ex)
            {
                sw.Stop();

                return new CryptoResult($"Error: {ex.Message}", sw.Elapsed.TotalMilliseconds);
            }
        }
    }
}
