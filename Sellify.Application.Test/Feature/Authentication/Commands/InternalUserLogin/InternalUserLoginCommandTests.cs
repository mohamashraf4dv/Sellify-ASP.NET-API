using FluentAssertions;
using Moq;
using Sellify.Application.Contracts.Services;
using Sellify.Application.Features.Authentication.Commands.InternalUserLogin;
using Sellify.Application.Test.Feature.Mocks;

namespace Sellify.Application.Test.Feature.Authentication.Commands.InternalUserLogin
{
    public class InternalUserLoginCommandTests
    {
        private Mock<IAuthenticationService> _authenticationServiceMock;
        public InternalUserLoginCommandTests()
        {
            _authenticationServiceMock = MockAuthenticationService.GetMockAuthenticationService();
        }
        [Fact]
        public async Task Handle_ValidData_ShouldReturnTokenWithStatus201()
        {
            //Arrange
            var handler = new InternalUserLoginCommandHandler(_authenticationServiceMock.Object);
            var command = new InternalUserLoginCommand() { userLoginDTO= new InternalUserLoginDTO() { LoginIdentifier= "mash11",IsPersistence= false,Password= "randomHash1" } };
            //Act
            var result = await handler.Handle(command, CancellationToken.None);

            //Assert
            result.data.Should().NotBeNull();
            result.statusCode.Should().Be(201);
        }
        [Fact]
        public async Task Handle_InvalidData_ShouldnotReturnTokenWithStatusCode404()
        {
            //Arrange
            var handler = new InternalUserLoginCommandHandler(_authenticationServiceMock.Object);
            var command = new InternalUserLoginCommand() { userLoginDTO = new InternalUserLoginDTO() { LoginIdentifier = "invalid", IsPersistence = false, Password = "invalidUserPassword" } };
            //Act
            var result = await handler.Handle(command, CancellationToken.None);

            //Assert
            result.data.Should().BeNull();
            result.statusCode.Should().Be(404);
        }
    }
}
