using EMixCepFinder.Domain.CepFinderClient;
using EMixCepFinder.Domain.Dto;
using EMixCepFinder.Domain.Service;

namespace EmixCepFinder.Service
{
    /// <summary>
    /// Implementation of the IViaCepService interface using Refit.
    /// </summary>
    public class ViaCepService : IViaCepService
    {
        private readonly IViaCepClient _viaCepClient;

        /// <summary>
        /// Creates a new instance of ViaCepService using the specified IViaCepClient.
        /// </summary>
        /// <param name="viaCepClient">An implementation of the IViaCepClient interface for making HTTP requests to the ViaCep API using Refit.</param>
        public ViaCepService(IViaCepClient viaCepClient)
        {
            _viaCepClient = viaCepClient;
        }

        /// <summary>
        /// Retrieves address information for the specified postal code from the ViaCep API.
        /// </summary>
        /// <param name="postalCode">The postal code to retrieve address information for.</param>
        /// <returns>An instance of the AddressInfoDto class containing the retrieved address information.</returns>
        public async Task<AddressInfoDto> GetAddressInfoAsync(string postalCode) =>
            await _viaCepClient.GetAddressInfoAsync(postalCode);
    }
}