using EMixCepFinder.Domain.Dto;
using EMixCepFinder.Domain.Model;

namespace EmixCepFinder.Service.Map
{
    /// <summary>
    /// Provides extension methods for mapping <see cref="AddressInfoDto"/> to <see cref="AddressInfo"/>.
    /// </summary>
    public static class AddressInfoMapper
    {
        /// <summary>
        /// Maps an <see cref="AddressInfoDto"/> instance to an <see cref="AddressInfo"/> instance.
        /// </summary>
        /// <param name="addressInfoDto">The <see cref="AddressInfoDto"/> instance to map.</param>
        /// <returns>An <see cref="AddressInfo"/> instance.</returns>
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