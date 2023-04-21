using EmixCepFinder.Service;
using EMixCepFinder.Domain.Dto;
using EMixCepFinder.Domain.Model;
using EMixCepFinder.Domain.Repository;
using EMixCepFinder.Domain.Service;
using FluentAssertions;
using Moq;

namespace EMixCepFinder.Service.UnitFacts
{
    public class CepFinderServiceUnitFacts
    {
        private readonly CepFinderService _cepFinderService;
        private readonly Mock<ICepFinderRepository> _cepFinderRepositoryMock;
        private readonly Mock<IViaCepService> _viaCepServiceMock;
        public CepFinderServiceUnitFacts()
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
        public async Task GetAddressInfo_WithValidCep_ShouldBeExpected(string postalCode)
        {
            //Arrange
            postalCode = postalCode.NormalizeCep();
            AddressInfoDto addressInfoDto = new()
            {
                Cep = postalCode,
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

            _viaCepServiceMock.Setup(s => s.GetAddressInfoAsync(postalCode)).Returns(Task.FromResult(addressInfoDto));

            //Act
            var result = await _cepFinderService.GetAddressInfo(postalCode);

            //Assert
            result.Should().BeOfType<AddressInfo>();
            result.PostalCode.Should().Be(postalCode);
            result.Neighborhood.Should().Be(addressInfoDto.Bairro);
            result.Complement.Should().Be(addressInfoDto.Complemento);
            result.DDD.Should().Be(addressInfoDto.Ddd);
            result.GIA.Should().Be(addressInfoDto.Gia);
            result.IBGE.Should().Be(int.TryParse(addressInfoDto.Ibge, out var i) ? i : 0);
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

        public async Task GetAddressInfo_WithInvalidCep_ShouldThrowArgumentException(string postalCode)
        {
            //Arrange

            //Act
            var action = async () => await _cepFinderService.GetAddressInfo(postalCode);

            //Assert
            await action.Should().ThrowAsync<ArgumentException>().Where(ex => ex.Message == "Postal code provided is not in a valid format");
        }

        [Theory]
        [InlineData("06340-345")]
        [InlineData("09099-999")]
        public async Task GetAddressInfo_WithInexistentCep_ShouldThrowArgumentException(string postalCode)
        {
            //Arrange
            var nonExistentAddressInfo = new AddressInfoDto { Erro = true };
            _viaCepServiceMock.Setup(s => s.GetAddressInfoAsync(postalCode)).Returns(Task.FromResult(nonExistentAddressInfo));

            //Act
            var action = async () => await _cepFinderService.GetAddressInfo(postalCode);

            //Assert
            await action.Should().ThrowAsync<ArgumentException>().Where(ex => ex.Message == "Postal code not found");
        }

        [Fact]
        public async Task GetAddressInfosByState_ValidState_ReturnsAddressInfos()
        {
            // Arrange
            var state = "SP";
            var expectedAddressInfos = new List<AddressInfo> { new AddressInfo { State = state } };
            _cepFinderRepositoryMock.Setup(r => r.SelectByState(state))
                                   .ReturnsAsync(expectedAddressInfos);

            // Act
            var result = await _cepFinderService.GetAddressInfosByState(state);

            // Assert
            result.Should().BeEquivalentTo(expectedAddressInfos);
        }

        [Fact]
        public async Task GetAddressInfosByState_InvalidState_ThrowsArgumentException()
        {
            // Arrange
            var state = "INVALID_STATE";

            // Act
            var act = async () => await _cepFinderService.GetAddressInfosByState(state);

            // Assert
            await act.Should().ThrowAsync<ArgumentException>()
                     .WithMessage($"Invalid state format: {state}.*");
        }

        [Fact]
        public async Task GetAddressInfosByState_StateWithNoAddressInfos_ThrowsArgumentException()
        {
            // Arrange
            var state = "SP";
            _cepFinderRepositoryMock.Setup(r => r.SelectByState(It.IsAny<string>()))
                           .ReturnsAsync(new List<AddressInfo>());

            // Act
            var act = async () => await _cepFinderService.GetAddressInfosByState(state);

            // Assert
            await act.Should().ThrowAsync<ArgumentException>()
                     .WithMessage($"There is no address info for the provided State. State: {state}");
        }
    }
}