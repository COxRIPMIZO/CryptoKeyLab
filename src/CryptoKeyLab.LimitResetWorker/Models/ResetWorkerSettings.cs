using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CryptoKeyLab.LimitResetWorker.Models
{
    public class ResetWorkerSettings
    {
        public int NoOfDataToFetch { get; set; } = 100; // Default value for the number of data to fetch
        public bool KeyStatus { get; set; } = true; // Default value for key status (true for active keys, false for inactive keys)
        public string ServiceStartDelay { get; set; } = "1M"; // Default value for service start delay
        public string ServiceStopDelay { get; set; } = "1M"; // Default value for service stop delay
    }
}
