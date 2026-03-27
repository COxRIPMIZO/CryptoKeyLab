using CryptoKeyLab.API.Filters;
using CryptoKeyLab.Domain.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CryptoKeyLab.API.Controllers.TestControllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProtectedController : ControllerBase
    {
        [HttpGet("test-security")] // This endpoint is protected by the ApiKeyAuthFilter, so only requests with a valid API key will reach this code.
        [ServiceFilter(typeof(ApiKeyAuthFilter))] // 🛡️ THE SHIELD! This one line locks the door.
        public IActionResult TestSecurity()
        {
            //// Because the filter passed, we KNOW this item exists in the context!
            var apiKeyInfo = (ApiKeyEntity)HttpContext.Items["ApiKeyDetails"]!;

            return Ok(new 
            {
                Message = "Congratulations! You have accessed a protected endpoint.",
                apiKeyInfo
            });
        }
    }
}
