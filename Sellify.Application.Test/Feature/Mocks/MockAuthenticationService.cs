using Microsoft.AspNetCore.Http;
using Moq;
using Sellify.Application.Contracts.Services;
using Sellify.Application.Features.Authentication.Commands.InternalUserLogin;
using Sellify.Application.Features.Authentication.Commands.UserRegisteration;
using Sellify.Application.Features.Token;
using Sellify.Application.Global;
using Sellify.Infrastructure.IdentityUserModel;
using Sellify.Infrastructure.Mapperly;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Sellify.Application.Test.Feature.Mocks
{
    public class MockAuthenticationService
    {
        public static Mock<IAuthenticationService> GetMockAuthenticationService()
        {
            List<ApplicationUser> applicationUsers = new List<ApplicationUser>()
            {
                new ApplicationUser(){Email="moash1@gmail.com",UserName= "mash11",Id="Guid_id1",PasswordHash="randomHash1"},
                new ApplicationUser(){Email="moash2@gmail.com",UserName= "mash12",Id="Guid_id2",PasswordHash="randomHash1"},
                new ApplicationUser(){Email="moash3@gmail.com",UserName= "mash13",Id="Guid_id3",PasswordHash="randomHash1"},
            };

            var mock = new Mock<IAuthenticationService>();

            mock.Setup(a => a.InternalLogin(It.IsAny<InternalUserLoginDTO>())).ReturnsAsync((InternalUserLoginDTO internalUserLogin) =>
            {
                var userFound = applicationUsers.FirstOrDefault(a => (a.Email == internalUserLogin.LoginIdentifier || a.UserName == internalUserLogin.LoginIdentifier) && a.PasswordHash == internalUserLogin.Password);
                if(userFound is null)
                    return new GenericResultDTO<TokensDTO>(null, 404);

                return new GenericResultDTO<TokensDTO>(new TokensDTO("refreshToken","accessToken"),201);

            });

            mock.Setup(a => a.Register(It.IsAny<UserRegisterationDTO>())).ReturnsAsync((UserRegisterationDTO userRegisteration) =>
            {
                ApplicationUser applicationUser = ApplicationUserMapper.UserRegisterationDtoToApplicationUser(userRegisteration);
                if(applicationUsers.Any(a=> a.Email == applicationUser.Email || a.UserName == applicationUser.UserName))
                    return new GenericResultDTO<TokensDTO>(null, 400);

                applicationUsers.Add(applicationUser);
                if(applicationUsers.Count==3)
                    return new GenericResultDTO<TokensDTO>(null, 400);

                return new GenericResultDTO<TokensDTO>(new TokensDTO("refreshToken","accessToken"),201);
            });

            return mock;
        }
    }
}
