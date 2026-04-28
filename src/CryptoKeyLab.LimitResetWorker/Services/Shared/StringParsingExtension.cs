using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace CryptoKeyLab.LimitResetWorker.Services.Shared
{
    public static class StringParsingExtension
    {
        /// <summary>
        /// Converts strings like "10s", "5m", "1h" into a TimeSpan object.
        /// </summary>
        public static TimeSpan ToTimeSpan(this string? value)
        {
            if (string.IsNullOrWhiteSpace(value))
                throw new ArgumentNullException(nameof(value), "Input string cannot be null or empty.");

            var lowerValue = value.Trim().ToLower();

            // Extract the numeric part and the unit part
            return lowerValue switch
            {
                var p when p.EndsWith("ms") && int.TryParse(p[..^2], out int ms) => TimeSpan.FromMilliseconds(ms),
                var p when p.EndsWith("s") && int.TryParse(p[..^1], out int s) => TimeSpan.FromSeconds(s),
                var p when p.EndsWith("m") && int.TryParse(p[..^1], out int m) => TimeSpan.FromMinutes(m),
                var p when p.EndsWith("h") && int.TryParse(p[..^1], out int h) => TimeSpan.FromHours(h),
                var p when p.EndsWith("d") && int.TryParse(p[..^1], out int d) => TimeSpan.FromDays(d),
                _ => throw new FormatException($"Invalid delay format: {value}")
            };
        }
    }
}
