using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CryptoKeyLab.API.Controllers.TestControllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ExceptionController : ControllerBase
    {
        [HttpGet("test-exception")]
        public IActionResult TestCrash()
        {
            throw new Exception("This is a test exception to demonstrate error handling in the API.");
        }
    }
}
