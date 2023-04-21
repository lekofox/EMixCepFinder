using EMixCepFinder.Domain.Service;
using Microsoft.AspNetCore.Mvc;

namespace EMixCepFinder.API.Controllers
{
    [ApiController]
    [Route("api/v1/")]
    public class AddressInfoController : ControllerBase
    {
        private readonly ICepFinderService _cepFinderService;

        public AddressInfoController(ICepFinderService cepFinderService)
        {
            _cepFinderService = cepFinderService;
        }

        /// <summary>
        /// Get AddressInfo from ViaCep API or Database by zip code.
        /// </summary>
        /// <param name="postalCode">Zip code to search for AddressInfo.</param>
        /// <returns>Returns AddressInfo related to given zip code.</returns>
        [HttpGet]
        [Route("CepFinder/{postalCode}")]
        [ProducesResponseType(typeof(string), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(string), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetAddressInfoByCep(string postalCode)
        {
            try
            {
                var result = await _cepFinderService.GetAddressInfo(postalCode);

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

        /// <summary>
        /// Get all AddressInfo related to a specific state from Database.
        /// </summary>
        /// <param name="state">State simplified name to search for all AddressInfo.</param>
        /// <returns>Returns all AddressInfo related to given simplified state name.</returns>
        [HttpGet]
        [Route("CepFinder/state/{state}")]
        [ProducesResponseType(typeof(string), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(string), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetAddressInfosByState(string state)
        {
            try
            {
                var result = await _cepFinderService.GetAddressInfosByState(state);

                return Ok();
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
