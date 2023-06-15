using EMixCepFinder.API.Controllers;
using EMixCepFinder.Domain.Caching;
using EMixCepFinder.Domain.Model;
using EMixCepFinder.Domain.Service;
using FluentAssertions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace EMixCepFinder.API.UnitTests.Controllers
{
    public class CepFinderControllerUnitTests
    {
        private readonly Mock<ICepFinderService> _cepFinderService;
        private readonly Mock<ICachingService> _cachingServiceMock;
        private AddressInfoController _addressInfoController;

        public CepFinderControllerUnitTests()
        {
            _cepFinderService = new Mock<ICepFinderService>();
            _cachingServiceMock = new Mock<ICachingService>();
            _addressInfoController = new AddressInfoController(_cepFinderService.Object, _cachingServiceMock.Object);
        }

        [Fact]
        public async Task GetAddressInfo_ShouldBeExpected()
        {
            //Arrange
            var cep = "anything";

            _cepFinderService.Setup(_ => _.GetAddressInfo(cep)).Returns(Task.FromResult(new AddressInfo()));

            var request = new Mock<HttpRequest>();
            request.Setup(x => x.Scheme).Returns("http");
            request.Setup(x => x.Host).Returns(HostString.FromUriComponent("http://localhost:8080"));
            request.Setup(x => x.PathBase).Returns(PathString.FromUriComponent("/api"));

            var httpContext = new Mock<HttpContext>();
            httpContext.Setup(x => x.Request).Returns(request.Object);
            httpContext.Setup(x => x.Response.Headers).Returns(new HeaderDictionary());

            var controllerContext = new ControllerContext()
            {
                HttpContext = httpContext.Object
            };

            //Act 
            var result = await _addressInfoController.GetAddressInfoByCep(cep);

            //Assert
            result.GetType().Should().Be(typeof(OkObjectResult));
        }

        [Fact]
        public async Task GetAddressInfo_Should_CatchException_WhenArgumentException()
        {
            //Arrange
            var cep = "anything";

            _cepFinderService.Setup(_ => _.GetAddressInfo(cep)).Throws<ArgumentException>();

            var request = new Mock<HttpRequest>();
            request.Setup(x => x.Scheme).Returns("http");
            request.Setup(x => x.Host).Returns(HostString.FromUriComponent("http://localhost:8080"));
            request.Setup(x => x.PathBase).Returns(PathString.FromUriComponent("/api"));

            var httpContext = new Mock<HttpContext>();
            httpContext.Setup(x => x.Request).Returns(request.Object);
            httpContext.Setup(x => x.Response.Headers).Returns(new HeaderDictionary());

            var controllerContext = new ControllerContext()
            {
                HttpContext = httpContext.Object
            };

            //Act 
            var result = await _addressInfoController.GetAddressInfoByCep(cep);

            //Assert
            result.GetType().Should().Be(typeof(ObjectResult));
        }
    }
}