using EMixCepFinder.Domain.CepFinderClient;
using EMixCepFinder.Domain.Dto;
using EMixCepFinder.Domain.Service;

namespace EmixCepFinder.Service
{
    public class ViaCepService : IViaCepService
    {
        private readonly IViaCepClient _viaCepClient;

        public ViaCepService(IViaCepClient viaCepClient)
        {
            _viaCepClient = viaCepClient;
        }

        public async Task<AddressInfoDto> GetAddressInfoAsync(string cep) => 
            await _viaCepClient.GetAddressInfoAsync(cep);
    }
}
