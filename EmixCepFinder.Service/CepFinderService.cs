using EmixCepFinder.Service.Map;
using EMixCepFinder.Domain.Model;
using EMixCepFinder.Domain.Repository;
using EMixCepFinder.Domain.Service;


namespace EmixCepFinder.Service
{
    /// <summary>
    /// Provides methods to retrieve information about a Brazilian postal code (CEP).
    /// </summary>
    public class CepFinderService : ICepFinderService
    {
        private readonly ICepFinderRepository _cepFinderRepository;
        private readonly IViaCepService _viaCepService;

        /// <summary>
        /// Initializes a new instance of the <see cref="CepFinderService"/> class.
        /// </summary>
        /// <param name="cepFinderRepository">The repository that provides access to the database.</param>
        /// <param name="viaCepService">The service that provides access to the ViaCEP API.</param>
        public CepFinderService(ICepFinderRepository cepFinderRepository,
                                IViaCepService viaCepService)
        {
            _cepFinderRepository = cepFinderRepository ?? throw new ArgumentNullException(nameof(cepFinderRepository));
            _viaCepService = viaCepService ?? throw new ArgumentNullException(nameof(viaCepService));
        }

        /// <summary>
        /// Creates a new address information record in the database.
        /// </summary>
        /// <param name="addressInfo">The address information to be saved.</param>
        private async Task CreateAddressInfo(AddressInfo addressInfo)
        {
            try
            {
                await _cepFinderRepository.Create(addressInfo);
            }
            catch (Exception ex) when (ex.InnerException.Message.StartsWith("Cannot insert duplicate key row in object"))
            {
                //Todo: Log already exist on db
            }
        }

        /// <summary>
        /// Retrieves the address information for the specified postal code.
        /// </summary>
        /// <param name="cep">The postal code to be searched.</param>
        /// <returns>The address information for the specified postal code.</returns>
        /// <exception cref="ArgumentException">Thrown when the postal code provided is not in a valid format or is not found.</exception>
        public async Task<AddressInfo> GetAddressInfo(string cep)
        {
            string normalizedCep = cep.NormalizeCep();
            if (!normalizedCep.IsValid())
                throw new ArgumentException("Postal code provided is not in a valid format");

            var addressInfo = await _cepFinderRepository.Select(normalizedCep);

            if (addressInfo != null)
                return addressInfo;

            var addressInfoDto = await _viaCepService.GetAddressInfoAsync(normalizedCep);

            if (addressInfoDto.Erro)
                throw new ArgumentException("Postal code not found");

            addressInfo = addressInfoDto.Map();
            await CreateAddressInfo(addressInfo);
            return addressInfo;
        }
    }
}