using EMixCepFinder.Domain.Model;
using EMixCepFinder.Infrastructure.Database.Context;
using EMixCepFinder.Infrastructure.Repository;
using FluentAssertions;
using Moq;

namespace EMixCepFinder.Infrastructure.UnitTests.Repository
{
    public class CepFinderRepositoryUnitTests
    {
        private Mock<AddressInfoContext> _contextMock;
        private readonly CepFinderRepository _cepFinderRepositoryMock;
        public CepFinderRepositoryUnitTests()
        {
            _contextMock = new Mock<AddressInfoContext>();
            _cepFinderRepositoryMock = new CepFinderRepository(_contextMock.Object);
            _contextMock.Setup(c => c.SaveChangesAsync(It.IsAny<CancellationToken>())).Returns(() => Task.Run(() => { return 1; })).Verifiable();
        }

        [Fact]
        public async Task Create_ShouldCallSaveChanges_Once()
        {
            //Arrange

            //Act
            await _cepFinderRepositoryMock.Create(new AddressInfo());

            //Assert
            _contextMock.Verify(s=> s.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once());
        }

        [Fact]
        public async Task Select_ShouldBeExpected()
        {
            //Arrange
            var cep = "anything";

            //Act
            var result = await _cepFinderRepositoryMock.Select(cep);

            //Assert
            result.Should().Be(new AddressInfo());
        }
    }
}
