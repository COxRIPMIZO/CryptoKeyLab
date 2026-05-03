using CryptoKeyLab.Domain.Interfaces.Encoding;
using CryptoKeyLab.Domain.Models.Cryptography;
using CryptoKeyLab.Domain.Models.Encoding;
using System.Diagnostics;

namespace CryptoKeyLab.Cryptography.Encode.Base
{
    public class Base64Encoder : IEncodingAlgorithm
    {
        public string? Name => "Base64";

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
                return new CryptoResult("Error: Invalid Base64 input string.",sw.Elapsed.TotalMilliseconds);
            }
        }

        public CryptoResult Encoding(EncodingOptions options)
        {
            //step 1.check the parmaters
            if (string.IsNullOrWhiteSpace(options.InputData))
                return new CryptoResult(string.Empty, 0);

            //step 2. create stop watch 
            Stopwatch sw = Stopwatch.StartNew();

            //step 3.get bytes of inputs
            Span<byte> bytes = System.Text.Encoding.UTF8.GetBytes(options.InputData);
            
            //step 4. convert it into base64
            var base64string = Convert.ToBase64String(bytes);

            sw.Stop();

            return new CryptoResult(base64string,sw.Elapsed.TotalMilliseconds);
        }
    }
}
