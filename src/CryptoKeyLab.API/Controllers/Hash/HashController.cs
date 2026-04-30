using CryptoKeyLab.API.Filters;
using CryptoKeyLab.Domain.Interfaces.Factories;
using CryptoKeyLab.Domain.Models.Cryptography;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CryptoKeyLab.API.Controllers.Hash
{
    [Route("api/[controller]")]
    [ApiController]

    //prevent this controller from abuse with the help of apiauth filter, so only requests with a valid API key will reach this code.
     [ServiceFilter(typeof(ApiKeyAuthFilter))] // 🛡️ THE SHIELD! This one line locks the door.
    public class HashController : ControllerBase
    {
        //injecting the _hashFactory to get the list of available algorithms
         private readonly IHashFactory _hashFactory;
 
         public HashController(IHashFactory hashfactory)
         {
             _hashFactory = hashfactory;
         }
 
        // Endpoint 1: Get the list for your UI dropdown[HttpGet("hash-algorithms")]
        [HttpGet("Algorithms")]
        public async Task<IActionResult> GetAvailableHashAlgorithms()
        {
            var hashAlgos = await _hashFactory.GetAvailableAlgorithmsAsync();

            // We can also add some custom formatting here if needed, for example, to return a more user-friendly name or additional metadata about each algorithm.
            var formattedAlgos = hashAlgos.Select(algo => new 
            {
               Name = algo.DisplayName,
               AlgoFamily = algo.Family,
               RequiresSalt = algo.RequiresSalt,
               RequiredKey = algo.RequiresKey,
               RequiredIteration = algo.RequiresIterations,
               IsSecure = algo.IsSecure,
               IsActive = algo.IsActive
            });

            return Ok(formattedAlgos);
        }

        //Endpoint 2.compute hash for hashing the input we get
        // We can also add some custom formatting here if needed, for example, to return a more user-friendly name or additional metadata about each algorithm.

        [HttpPost("ComputeHash")]
        public async Task<IActionResult> ComputeHash([FromQuery] string algorithmName, [FromBody] HashOptions hashOptions)
        {
            try
            {
                //Step 1.creating the valid hash from endpoint input
                var hashClass = await _hashFactory.CreateAsync(algorithmName);

                //step 2.Compute the hash using the provided options
                var result = hashClass.ComputeHash(hashOptions);

                //step 3. return the result to the client
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
