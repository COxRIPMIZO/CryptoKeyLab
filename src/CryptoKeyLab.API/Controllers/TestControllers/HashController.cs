using CryptoKeyLab.API.Filters;
using CryptoKeyLab.Domain.Interfaces.Factories;
using CryptoKeyLab.Domain.Models.Cryptography;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CryptoKeyLab.API.Controllers.TestControllers
{
    [Route("api/[controller]")]
    [ApiController]
    [ServiceFilter(typeof(ApiKeyAuthFilter))]
    public class HashController : ControllerBase
    {
        private readonly IHashFactory _hashFactory;

        public HashController(IHashFactory hashfactory)
        {
            _hashFactory = hashfactory;
        }

        // Endpoint 1: Get the list for your UI dropdown[HttpGet("hash-algorithms")]
        [HttpGet("get-algos")]
        public async Task<IActionResult> GetAvailableHashAlgorithms()
        {
            var algorithms = await _hashFactory.GetAvailableAlgorithmsAsync();
            return Ok(algorithms);
        }

        // Endpoint 2: Hash a sample input using the specified algorithm [HttpPost("hash-input")]
        [HttpPost("hash")]
        public async Task<IActionResult> ComputeHash([FromQuery] string hashAlgorith,[FromBody]HashOptions hashOptions)
        {
            try
            {
                // 1. Get the requested algorithm from the Factory (e.g., "SHA-256")
                var algorithm = await _hashFactory.CreateAsync(hashAlgorith);

                //step 2.Start hashing
                var result = algorithm.ComputeHash(hashOptions);

                //step 3. return the result to the client
                return Ok(result);
            }
            catch (NotSupportedException ex)
            {
                // If the user types an algorithm that doesn't exist, we return a 400 Bad Request
                return BadRequest(new { Message = ex.Message });
            }
        }
    }
}
