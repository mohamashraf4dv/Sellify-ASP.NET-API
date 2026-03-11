using FluentAssertions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Moq;
using Sellify.Application.Contracts;
using Sellify.Application.Features.Authentication.Commands.InternalUserLogin;
using Sellify.Application.Global;
using Sellify.Infrastructure.IdentityUserModel;

namespace Sellify.Infrastructure.Tests
{
    public class AuthenticationRepositoryTests
    {
        [Fact]
        public async Task InternalLogin_WhenUserIsNull_ReturnsStatusCode404NotfoundWithInvalidCredentials()
        {
            //Arrange
            InternalUserLoginDTO userLoginDTO = new InternalUserLoginDTO() { IsPersistence = true, LoginIdentifier = null, Password = null };
            var moq = new Mock<IAuthenticationRepository>();
            var setup = moq.Setup(repo => repo.InternalLogin(userLoginDTO)).ReturnsAsync(new GenericResultDTO(data: null, statusCode: StatusCodes.Status404NotFound, errorsKeyValues: new Dictionary<string, HashSet<string>> { { "Credentials", new HashSet<string> { "invalid credentials" } } }));

            //Act
            var result = await moq.Object.InternalLogin(userLoginDTO);

            //Assert
            result.Should().BeEquivalentTo(new GenericResultDTO(data: null, statusCode: StatusCodes.Status404NotFound, errorsKeyValues: new Dictionary<string, HashSet<string>> { { "Credentials", new HashSet<string> { "invalid credentials" } } } ));
        }

    }
}
