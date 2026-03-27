using CryptoKeyLab.Core.Services;
using CryptoKeyLab.Domain.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace CryptoKeyLab.API.Filters
{
    public class ApiKeyAuthFilter : IAsyncActionFilter
    {
        private readonly string _apiKeyHeaderName;

        //inject the new core service
        private readonly IApiKeyValidationService _apiKeyValidationService;
        //DI Injection
        public ApiKeyAuthFilter(IApiKeyValidationService apiKeyValidation, IConfiguration configuration)
        {
            _apiKeyValidationService = apiKeyValidation;

            // Read from appsettings.json dynamically! (With a safe fallback just in case)
            _apiKeyHeaderName = configuration["ApiSettings:ApiKeyHeaderName"] ?? "X-API-KEY";
        }
        
        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            //===============================================
            //Step 1: check if keyheader is exist in the request or not 
            //===============================================
            bool isKeyHeaderExist = context.HttpContext.Request.Headers.TryGetValue(_apiKeyHeaderName, out var headerValue);
            if (!isKeyHeaderExist)
            {
                context.Result = new UnauthorizedObjectResult(new { Message = $"Access Denied: Missing {_apiKeyHeaderName} header." });

                return;
            }

            //===============================================
            //Step 2 : Let the Core Service do all the heavy lifting!
            //===============================================
            var keyValidationResult = await _apiKeyValidationService.ValidateAndConsumeKeyAsync(headerValue);

            //===============================================
            //Step 3 : Handle the HTTP Response based on the result
            //===============================================
            if (!keyValidationResult.IsValid)
            {
                //Return result based on the reason of invalidity of rete limit
                if (keyValidationResult.IsReateLimitExceeded)
                {
                    context.Result = new ObjectResult(new { Message = keyValidationResult.Message }) { StatusCode = StatusCodes.Status429TooManyRequests};
                    return;
                }

                ////Return result based on the reason of invalidity of api validation failed
                context.Result = new UnauthorizedObjectResult(new { Message = keyValidationResult.Message ?? "Access Denied: Invalid API Key." });
                return;
            }

            //===============================================
            //Step 4 : Pass context and proceed
            //===============================================
            context.HttpContext.Items["ApiKeyDetails"] = keyValidationResult.KeyEntity; // Store the key details for later use in the request pipeline

            await next();
        }
    }
}
