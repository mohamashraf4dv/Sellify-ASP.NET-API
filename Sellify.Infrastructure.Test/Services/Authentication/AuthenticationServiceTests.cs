using FluentAssertions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Moq;
using Sellify.Application.Contracts.Services;
using Sellify.Application.Features.Authentication.Commands.UserRegisteration;
using Sellify.Application.Features.Token;
using Sellify.Application.Global;
using Sellify.Infrastructure.IdentityUserModel;
using Sellify.Infrastructure.Mapperly;

namespace Sellify.Infrastructure.Test.Services.Authentication
{
    public class AuthenticationServiceTests
    {
        [Fact]
        public async Task Register_WhenValidData_ShouldCreateTokens()
        {
            //Arrange
            UserRegisterationDTO user = new UserRegisterationDTO("Mohamed", "Ashraf", "moash4dv", "moash4dv@gmail.com", "randomText", "07-10-1997");
            ApplicationUser applicationUser = ApplicationUserMapper.UserRegisterationDtoToApplicationUser(user);

            var userManagerMock = new Mock<IUserStore<ApplicationUser>>();
            var jwtTokenService = new Mock<ITokenService>();

            userManagerMock
                .Setup(x => x.CreateAsync(It.IsAny<ApplicationUser>(), It.IsAny<CancellationToken>() )).ReturnsAsync(IdentityResult.Success);

            jwtTokenService
                .Setup(x => x.GenerateTokens(It.IsAny<string>())).ReturnsAsync(new TokensDTO("123", "123"));

            //Act
            IdentityResult result = await userManagerMock.Object.CreateAsync(applicationUser, new CancellationToken());

            TokensDTO tokenDto = await jwtTokenService.Object.GenerateTokens("loginSuccess");

            //Assert
            result.Succeeded.Should().BeTrue();
            tokenDto.Should().NotBeNull();

        }
        [Fact]
        public async Task Register_WhenNotValidData_ShouldNotCreateTokens()
        {
            //Arrange
            UserRegisterationDTO user = new UserRegisterationDTO("Mohamed", "Ashraf", "moash4dv", "moash4dv@gmail.com", "randomText", "07-10-1997");
            ApplicationUser applicationUser = ApplicationUserMapper.UserRegisterationDtoToApplicationUser(user);

            var userManagerMock = new Mock<IUserStore<ApplicationUser>>();
            var jwtTokenService = new Mock<ITokenService>();

            userManagerMock
                .Setup(x => x.CreateAsync(It.IsAny<ApplicationUser>(), It.IsAny<CancellationToken>())).ReturnsAsync(IdentityResult.Failed());

            jwtTokenService
                .Setup(x => x.GenerateTokens(It.IsAny<string>())).ReturnsAsync(null as TokensDTO);

            //Act
            IdentityResult result = await userManagerMock.Object.CreateAsync(applicationUser, new CancellationToken());

            TokensDTO tokenDto = await jwtTokenService.Object.GenerateTokens("loginFailure");

            //Assert
            result.Succeeded.Should().BeFalse();
            tokenDto.Should().BeNull();

        }
    }
}
