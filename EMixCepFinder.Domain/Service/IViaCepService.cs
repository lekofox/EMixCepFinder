using EMixCepFinder.Domain.Dto;

namespace EMixCepFinder.Domain.Service
{
    public interface IViaCepService
    {
        public Task<AddressInfoDto> GetAddressInfoAsync(string postalCode);
    }
}
