using CryptoKeyLab.API.Filters;
using CryptoKeyLab.Domain.Interfaces.Factories;
using CryptoKeyLab.Domain.Models.Encoding;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Org.BouncyCastle.Asn1.Mozilla;

namespace CryptoKeyLab.API.Controllers.Encoding
{
    [Route("api/[controller]")]
    [ApiController]

    //Key Validation Before Accessing Any Endpoint in this Controller
    [ServiceFilter(typeof(ApiKeyAuthFilter))]
    public class EncodingController : ControllerBase
    {
        private readonly IEncodingFactory _encodingFactory;

        public EncodingController(IEncodingFactory encodingFactory)
        {
            _encodingFactory = encodingFactory;
        }

        // Endpoint to Get Available Encoding Algorithms
        [HttpGet("Algorithms")]
        public async Task<IActionResult> GetAvailableEncodingAlgorithms()
        {
            var algorithms = await _encodingFactory.GetAvailableAlgorithmsAsync();

            //modify the reasult according to the needs of the frontend, for example we can return only the names of the algorithms instead of the whole objects
            //var formattedRes = algorithms.Select(algo => new 
            //{
            //    Id = algo.Id,
            //    Name = algo.DisplayName,
            //    Category = algo.Category,
            //    Family = algo.Family,
            //    IsActive = algo.IsActive,
            //    SortOrder = algo.SortOrder
            //});

            //return Ok(formattedRes);
            return Ok(algorithms);
        }

        // Endpoint to Perform Encoding Based on the Provided Algorithm and Options
        [HttpPost("Encode")]
        public async Task<IActionResult> EncodeData([FromQuery] string AlgoName, [FromBody] EncodingOptions encodingOptions)
        {
            var encodingAlgoClass = await _encodingFactory.CreateAsync(AlgoName);

            // Check if the algorithm class was successfully created
            if (encodingAlgoClass == null)
            {
                return BadRequest($"Encoding algorithm '{AlgoName}' not found.");
            }

            // Perform the encoding operation using the provided options
            var result = encodingAlgoClass.Encoding(encodingOptions);

            return Ok(result);
        }

        // Endpoint to Perform Decoding Based on the Provided Algorithm and Options
        [HttpPost("Decode")]
        public async Task<IActionResult> DecodeData([FromQuery] string decodeAlgo, [FromBody] EncodingOptions decodingOptions)
        {
            //check and create instance of the decoding algorithm class based on the provided name
            var decodeAlgoClass = await _encodingFactory.CreateAsync(decodeAlgo);

            //check if the algorithm class was successfully created
            if(decodeAlgoClass is null)
            {
                return BadRequest($"Decoding algorithm '{decodeAlgo}' not found.");
            }

            //perform the decoding operation using the provided options and algorithm name
            var result  = decodeAlgoClass.Decoding(decodingOptions);

            return Ok(result);
        }
    }
}
