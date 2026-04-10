using Microsoft.AspNetCore.Identity;
using Moq;
using Sellify.Application.Contracts.Services;
using Sellify.Application.Features.Authentication.Commands.UpdateUserProfile;
using Sellify.Application.Features.Authentication.Query.GetUserProfile;
using Sellify.Application.Global;
using Sellify.Infrastructure.IdentityUserModel;
using Sellify.Infrastructure.Mapperly;

namespace Sellify.Application.Test.Feature.Mock
{
    public class MockUserService
    {
        public static Mock<IUserService> GetMockUserService()
        {
            var applicationUsers = new List<ApplicationUser>()
            {
                new ApplicationUser(){Email="moash22@gmail",FirstName="mohamed",LastName="ashraf",Id="Guid-1",UserName="userName1"},
                new ApplicationUser(){Email="moash12@gmail",FirstName="mohamed",LastName="ashraf",Id="Guid-2",UserName="userName2"},
            };

            var mock = new Mock<IUserService>();
            mock.Setup(u => u.UpdateUserProfile(It.IsAny<UpdateUserProfileDTO>())).ReturnsAsync((UpdateUserProfileDTO updateUser) =>
            {
                var userFound = applicationUsers.FirstOrDefault(a => a.Email == updateUser.Email);
                if (userFound is null)
                    return new GenericResultDTO(null, 404);

                ApplicationUserMapper.UpdateUserProfileToApplicationUser(updateUser, userFound);

                return new GenericResultDTO(null, 200);
            });

            mock.Setup(u => u.GetUserInformationById(It.IsAny<string>())).ReturnsAsync((string userId) =>
            {
                var applicationUser = applicationUsers.FirstOrDefault(a => a.Id == userId);
                if (applicationUser is null)
                    return new GenericResultDTO<GetUserProfileQueryDTO>(null, 404);

                var userProfileDTO = new GetUserProfileQueryDTO(applicationUser.FirstName, applicationUser.LastName, applicationUser.Email!, applicationUser.UserName!, applicationUser?.PhoneNumber, null, applicationUser?.SellerRoleRequestStatus.ToString());
                return new GenericResultDTO<GetUserProfileQueryDTO>(userProfileDTO, 200);
            });
            return mock;
        }
    }
}
