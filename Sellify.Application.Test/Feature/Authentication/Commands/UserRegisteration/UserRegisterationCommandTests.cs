using FluentAssertions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Moq;
using Sellify.Application.Contracts.Services;
using Sellify.Application.Features.Authentication.Commands.UserRegisteration;
using Sellify.Application.Features.Token;
using Sellify.Application.Test.Feature.Mocks;
using Sellify.Infrastructure.IdentityUserModel;
using Sellify.Infrastructure.Mapperly;
namespace Sellify.Application.Test.Feature.Authentication.Commands.UserRegisteration
{
    public class UserRegisterationCommandTests
    {
        private Mock<IAuthenticationService> _authenticationService;
        public UserRegisterationCommandTests()
        {
            _authenticationService = MockAuthenticationService.GetMockAuthenticationService();
        }
        [Fact]
        public async Task Handle_WhenValidData_ShouldCreateNewUser()
        {
            //Arrange
            UserRegisterationCommand command = new UserRegisterationCommand()
                { userRegisteration= new UserRegisterationDTO("firstName","lastName","userName","email@gmail.com","password","07-10-1997")};
            UserRegisterationCommandHandler handler = new UserRegisterationCommandHandler(_authenticationService.Object);

            //Act
            var result = await handler.Handle(command,CancellationToken.None);

            //Assert
            result.data.Should().NotBeNull();
            result.statusCode.Should().Be(201);
        }

        [Fact]
        public async Task Handle_WhenExistingUser_ShouldnotCreateUser()
        {
            //Arrange
            UserRegisterationCommand command = new UserRegisterationCommand()
            { userRegisteration = new UserRegisterationDTO("firstName", "lastName", "mash11", "moash1@gmail.com", "password", "07-10-1997") };
            UserRegisterationCommandHandler handler = new UserRegisterationCommandHandler(_authenticationService.Object);

            //Act
            var result = await handler.Handle(command, CancellationToken.None);

            //Assert
            result.data.Should().BeNull();
            result.statusCode.Should().Be(400);
        }
    }
}
