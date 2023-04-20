using EmixCepFinder.Service;
using EmixCepFinder.Service.Extensions;
using EMixCepFinder.Domain.Dto;
using EMixCepFinder.Domain.Model;
using EMixCepFinder.Domain.Repository;
using EMixCepFinder.Domain.Service;
using FluentAssertions;
using Moq;

namespace EMixCepFinder.Service.UnitTests
{
    public class CepFinderServiceUnitTests
    {
        private readonly CepFinderService _cepFinderService;
        private readonly Mock<ICepFinderRepository> _cepFinderRepositoryMock;
        private readonly Mock<IViaCepService> _viaCepServiceMock;
        public CepFinderServiceUnitTests()
        {
            _cepFinderRepositoryMock = new Mock<ICepFinderRepository>();
            _viaCepServiceMock = new Mock<IViaCepService>();
            _cepFinderService = new CepFinderService(_cepFinderRepositoryMock.Object, _viaCepServiceMock.Object);
        }

        [Theory]
        [InlineData("06340-340")]
        [InlineData("06340340")]
        [InlineData("08246025")]
        [InlineData("08246-025")]
        public async Task GetAddressInfo_WithValidCep_ShouldBeExpected(string cep)
        {
            //Arrange
            cep = cep.NormalizeCep();
            AddressInfoDto addressInfoDto = new()
            {
                Cep = cep,
                Bairro = "Any neighborhood",
                Complemento = "",
                Ddd = "11",
                Gia = "any",
                Ibge = "any ibge",
                Localidade = "any city",
                Logradouro = "any street",
                Siafi = "111",
                Uf = "any state"
            };

            _viaCepServiceMock.Setup(s=> s.GetAddressInfoAsync(cep)).Returns(Task.FromResult(addressInfoDto));

            //Act
            var result = await _cepFinderService.GetAddressInfo(cep);

            //Assert
            result.Should().BeOfType<AddressInfo>();
            result.PostalCode.Should().Be(cep);
            result.Neighborhood.Should().Be(addressInfoDto.Bairro);
            result.Complement.Should().Be(addressInfoDto.Complemento);
            result.DDD.Should().Be(addressInfoDto.Ddd);
            result.GIA.Should().Be(addressInfoDto.Gia);
            result.IBGE.Should().Be(int.TryParse(addressInfoDto.Ibge, out var i) ? i :0);
            result.City.Should().Be(addressInfoDto.Localidade);
            result.Street.Should().Be(addressInfoDto.Logradouro);
            result.Unit.Should().Be(long.TryParse(addressInfoDto.Siafi, out var l) ? l : 0);
            result.State.Should().Be(addressInfoDto.Uf);
        }

        [Theory]
        [InlineData("anywrongceep")]
        [InlineData("012345-77")]
        [InlineData("012345----")]
        [InlineData("ImEMixCep")]

        public async Task GetAddressInfo_WithInvalidCep_ShouldThrowArgumentException(string cep)
        {
            //Arrange

            //Act
            var action = async () => await _cepFinderService.GetAddressInfo(cep);

            //Assert
            await action.Should().ThrowAsync<ArgumentException>().Where(ex => ex.Message == "Postal code provided is not in a valid format");
        }
        
        [Theory]
        [InlineData("06340-345")]
        [InlineData("09099-999")]
        public async Task GetAddressInfo_WithInexistentCep_ShouldThrowArgumentException(string cep)
        {
            //Arrange
            var nonExistentAddressInfo = new AddressInfoDto { Erro = true };
            _viaCepServiceMock.Setup(s => s.GetAddressInfoAsync(cep)).Returns(Task.FromResult(nonExistentAddressInfo));

            //Act
            var action = async () => await _cepFinderService.GetAddressInfo(cep);

            //Assert
            await action.Should().ThrowAsync<ArgumentException>().Where(ex => ex.Message == "Postal code not found");
        }
    }
}