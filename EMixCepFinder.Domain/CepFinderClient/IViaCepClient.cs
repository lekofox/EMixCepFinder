using EMixCepFinder.Domain.Dto;
using Refit;

namespace EMixCepFinder.Domain.CepFinderClient
{
    public interface IViaCepClient
    {
        [Get("/{cep}/json")]
        Task<AddressInfoDto> GetAddressInfoAsync(
           [Query("cep")] string cep);
    }
}
