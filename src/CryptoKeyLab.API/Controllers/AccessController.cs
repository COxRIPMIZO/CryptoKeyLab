using CryptoKeyLab.Domain.Interfaces;
using CryptoKeyLab.Domain.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CryptoKeyLab.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccessController : ControllerBase
    {
        private readonly IApiKeyService _apiKeyService;

        //Dependency injection of the IApiKeyService to generate temporary API keys
        public AccessController(IApiKeyService apiKeyService)
        {
            _apiKeyService = apiKeyService;
        }

        ///<summary>
        ///Generate temporay rate limit api key for free anonymous users, valid for 24 hours with a rate limit of 30 requests per minute. This endpoint allows users to obtain a temporary API key that can be used to authenticate their requests to the API. The generated key is securely stored in the database, and the service ensures that only valid and active keys are accepted for authentication. This feature provides a convenient way for users to access the API without requiring a full registration process, while still maintaining security and control over API usage.
        ///</summary>
        [HttpPost("generate-temp-key")]
        [ProducesResponseType(typeof(TemporarykeyResponse),statusCode:StatusCodes.Status201Created)]
        public async Task<IActionResult> GetApiKey()
        {
            // The controller is "dumb". It does no logic. It just asks the Core for the key.
            var response = await _apiKeyService.GenerateTemporaryKeyAsync();

            return Created(string.Empty, response);
        }
    }
}
