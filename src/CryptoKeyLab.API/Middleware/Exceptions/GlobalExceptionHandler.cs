using CryptoKeyLab.Domain.Models;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using System.Diagnostics;

namespace CryptoKeyLab.API.Middleware.Exceptions
{
    // IExceptionHandler is the modern .NET 9 Enterprise standard!
    public class GlobalExceptionHandler : IExceptionHandler
    {
        private readonly ILogger<GlobalExceptionHandler> _logger;

        // Dependency Injection: We inject the Logger so we can secretly record the real error
        public GlobalExceptionHandler(ILogger<GlobalExceptionHandler> log)
        {
            _logger = log;
        }
        public async ValueTask<bool> TryHandleAsync(HttpContext httpContext, Exception exception, CancellationToken cancellationToken)
        {
            //Step 1. LOG THE REAL ERROR (For your eyes only, saved to server logs)
            _logger.LogError(exception, "CRITICAL SYSTEM FAILURE: {Message}",exception.Message);

            //Step 2. Hide the real error and return a generic message to the client (Because we don't want to give hackers clues about our system)
            var errorResponse = new ErrorResponse(
                StatusCode: StatusCodes.Status500InternalServerError,
                Title: "Internal Server Error",
                Detail: "An unexpected error occurred in the CryptoKeyLab engine. Our team has been notified."
            );

            //Step 3.Set the HTTP response
            httpContext.Response.StatusCode = errorResponse.StatusCode;
            httpContext.Response.ContentType = "application.json";

            //Step 4. return the clean json result to the client
            await httpContext.Response.WriteAsJsonAsync(errorResponse,cancellationToken);

            //step 5. RETURN TRUE to tell .NET: "I caught it! Don't crash the app!"
            return true;
        }
    }
}
