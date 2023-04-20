using EmixCepFinder.Service;
using EMixCepFinder.Domain.CepFinderClient;
using EMixCepFinder.Domain.Dto;
using FluentAssertions;
using Moq;

namespace EMixCepFinder.Service.UnitTests
{
    public class ViaCepServiceUnitTests
    {
        private readonly Mock<IViaCepClient> _viaCepClientMock;
        private readonly ViaCepService _viaCepService;

        public ViaCepServiceUnitTests()
        {
            _viaCepClientMock = new Mock<IViaCepClient>();
            _viaCepService = new ViaCepService(_viaCepClientMock.Object);
        }

        [Fact]
        public async Task ViaCepService_WhenCalled_ShouldBeExpected()
        {
            //Arrange
            var cep = "any";
            var expectedResult = new AddressInfoDto
            {
                Cep = cep
            };

            _viaCepClientMock.Setup(s => s.GetAddressInfoAsync(cep)).Returns(Task.FromResult(expectedResult));

            //Act
            var result = await _viaCepService.GetAddressInfoAsync(cep);

            //Assert
            result.Should().BeOfType<AddressInfoDto>();
            result.Should().Be(expectedResult);
        }
    }
}
