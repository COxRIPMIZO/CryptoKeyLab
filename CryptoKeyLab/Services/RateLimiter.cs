namespace CryptoKeyLab.Services
{
    public class RateLimiter
    {
        private readonly int _maxRequests;
        private readonly TimeSpan _timeWindow;
        private readonly Dictionary<string, (int Count, DateTime RequestStart)> _requests = new();
        private readonly object _lock = new();
        public RateLimiter(int maxRequests, TimeSpan timeWindow)
        {
            _maxRequests = maxRequests;
            _timeWindow = timeWindow;
        }
        public bool IsRequestAllowed(string clientIP)
        {
            var now = DateTime.UtcNow;

            lock (_lock)
            {
                if(_requests.TryGetValue(clientIP,out var entry))
                {
                    //check if current time is within the time window
                    if(now - entry.RequestStart >= _timeWindow)
                    {
                        //reset count and start time
                        _requests[clientIP] = (1, now);
                        return true; // Request is allowed
                    }

                    //still within time window, check if count is less than max requests
                    if(entry.Count < _maxRequests)
                    {
                        _requests[clientIP] = (entry.Count + 1, entry.RequestStart);
                        return true; // Request is allowed
                    }

                    // Request is denied u untill time window expires
                    return false;
                }
                else
                {
                    //first request from this IP, add to dictionary
                    _requests[clientIP] = (1, now);
                    return true; // Request is allowed
                }
            }
        }
    }
}
