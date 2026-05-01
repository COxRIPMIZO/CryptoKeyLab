using CryptoKeyLab.API.Filters;
using CryptoKeyLab.Domain.Interfaces.Encoding;
using CryptoKeyLab.Domain.Interfaces.Factories;
using CryptoKeyLab.Domain.Models.Encoding;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CryptoKeyLab.API.Controllers.TestControllers
{
    [Route("api/[controller]")]
    [ApiController]
    [ServiceFilter(typeof(ApiKeyAuthFilter))]
    public class TestEncodingController : ControllerBase
    {
        private readonly IEncodingFactory _encodingFactory;

        public TestEncodingController(IEncodingFactory encodingFactory)
        {
            _encodingFactory = encodingFactory;
        }

        [HttpGet("EncodingsAlgorithms")]
        public async Task<IActionResult> GetEncoding()
        {
            var algo = await _encodingFactory.GetAvailableAlgorithmsAsync();

           return Ok(algo);
        }

        [HttpPost("TestEncode")]
        public async Task<IActionResult> Encode([FromQuery] string encodingAlgoName, [FromBody] EncodingOptions encodingOptions) 
        {
            var s = await _encodingFactory.CreateAsync(encodingAlgoName);

            var d = s.Encoding(encodingOptions);

            return Ok(d);
        }

        [HttpPost("TestDecoding")]
        public async Task<IActionResult> Decode([FromQuery] string encodingAlgoName, [FromBody] EncodingOptions encodingOptions)
        {
            var s = await _encodingFactory.CreateAsync(encodingAlgoName);

            var d = s.Decoding(encodingOptions);

            return Ok(d);
        }
    }
}
