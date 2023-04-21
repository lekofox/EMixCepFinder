using EMixCepFinder.Domain.Service;
using Microsoft.AspNetCore.Mvc;

namespace EMixCepFinder.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AddressInfoController : ControllerBase
    {
        private readonly ICepFinderService _cepFinderService;

        public AddressInfoController(ICepFinderService cepFinderService)
        {
            _cepFinderService = cepFinderService;
        }

        /// <summary>
        /// Get AddressInfo from ViaCep API by zip code.
        /// </summary>
        /// <param name="cep">Zip code to search for AddressInfo.</param>
        /// <returns>Returns AddressInfo related to given zip code.</returns>
        [HttpGet]
        [ProducesResponseType(typeof(string), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(string), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetAddressInfoByCep(string cep)
        {
            try
            {
                var result = await _cepFinderService.GetAddressInfo(cep);

                return Ok(result);
            }
            catch (ArgumentException ex)
            {
                return StatusCode(StatusCodes.Status400BadRequest, ex.Message);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Something went wrong. Please try again.");
            }

        }
    }
}