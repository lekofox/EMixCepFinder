using EMixCepFinder.Domain.Dto;
using EMixCepFinder.Domain.Model;

namespace EmixCepFinder.Service.Map
{
    public static class AddressInfoMapper
    {
        public static AddressInfo Map(this AddressInfoDto addressInfoDto) => new()
        {
            PostalCode = addressInfoDto.Cep,
            City = addressInfoDto.Localidade,
            Complement = addressInfoDto.Complemento,
            Neighborhood = addressInfoDto.Bairro,
            State = addressInfoDto.Uf,
            Street = addressInfoDto.Logradouro,
            Unit = long.TryParse(addressInfoDto.Siafi, out long unitResult) ? unitResult : 0,
            GIA = addressInfoDto.Gia,
            IBGE = int.TryParse(addressInfoDto.Ibge, out int ibgeResult) ? ibgeResult : 0,
            DDD = addressInfoDto.Ddd,
        };
    }
}
