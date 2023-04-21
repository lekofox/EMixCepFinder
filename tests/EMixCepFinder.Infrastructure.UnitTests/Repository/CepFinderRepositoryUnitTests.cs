using EMixCepFinder.Domain.Model;
using EMixCepFinder.Domain.Repository;
using EMixCepFinder.Infrastructure.Database.Context;
using EMixCepFinder.Infrastructure.Repository;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;

namespace EMixCepFinder.Infrastructure.UnitTests.Repository
{
    public class CepFinderRepositoryUnitTests
    {
        private readonly AddressInfoContext _context;
        private readonly ICepFinderRepository _repository;

        public CepFinderRepositoryUnitTests()
        {
            var options = new DbContextOptionsBuilder<AddressInfoContext>()
                .UseInMemoryDatabase(databaseName: "TestDatabase")
                .Options;

            _context = new AddressInfoContext(options);
            _repository = new CepFinderRepository(_context);
        }

        [Fact]
        public async Task Create_ShouldAddAddressInfoToDatabase()
        {
            // Arrange
            var addressInfo = new AddressInfo
            {
                PostalCode = "12345678",
                State = "SP",
                City = "São Paulo",
                Neighborhood = "Bela Vista",
                Street = "Avenida Paulista",
                Complement = "Apto 123",
                DDD = "11",
                GIA = "1234",
                IBGE = 12345,
                Unit = 987654,
            };

            // Act
            await _repository.Create(addressInfo);
            var result = await _repository.Select("12345678");

            // Assert
            result.Should().NotBeNull();
            result.Should().BeEquivalentTo(addressInfo, options => options
                .Excluding(x => x.Id));
        }

        [Fact]
        public async Task Select_ShouldReturnAddressInfoByPostalCode()
        {
            // Arrange
            var addressInfo = new AddressInfo
            {
                PostalCode = "12345678",
                State = "SP",
                City = "São Paulo",
                Neighborhood = "Bela Vista",
                Street = "Avenida Paulista",
                Complement = "Apto 123",
                DDD = "11",
                GIA = "1234",
                IBGE = 12345,
                Unit = 987654,
            };

            await _repository.Create(addressInfo);

            // Act
            var result = await _repository.Select("12345678");

            // Assert
            result.Should().NotBeNull();
            result.Should().BeEquivalentTo(addressInfo);
        }
    }
}
