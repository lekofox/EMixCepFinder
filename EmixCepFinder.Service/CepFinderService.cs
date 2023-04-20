using EmixCepFinder.Service.Extensions;
using EmixCepFinder.Service.Map;
using EMixCepFinder.Domain.Model;
using EMixCepFinder.Domain.Repository;
using EMixCepFinder.Domain.Service;

namespace EmixCepFinder.Service
{
    public class CepFinderService : ICepFinderService
    {
        private readonly ICepFinderRepository _cepFinderRepository;
        private readonly IViaCepService _viaCepService;

        public CepFinderService(ICepFinderRepository cepFinderRepository,
            IViaCepService viaCepService)
        {
            _cepFinderRepository = cepFinderRepository;
            _viaCepService = viaCepService;
        }

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
