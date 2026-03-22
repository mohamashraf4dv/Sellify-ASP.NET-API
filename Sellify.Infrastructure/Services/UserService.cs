
using Sellify.Application.Features.Authentication.Commands.UpdateUserProfile;
using Sellify.Application.Features.Authentication.Query.GetUserProfile;
using Sellify.Application.Global;
using Sellify.Infrastructure.Mapperly;

namespace Sellify.Infrastructure.Services
{
    public class UserService : IUserService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ITokenService _tokenService;

        public UserService(UserManager<ApplicationUser> userManager , ITokenService tokenService)
        {
            this._userManager = userManager;
            this._tokenService = tokenService;
        }
        public async Task<GenericResultDTO> UpdateUserProfile(UpdateUserProfileDTO updateUserProfile)
        {
            ApplicationUser user = await _userManager.FindByEmailAsync(updateUserProfile.Email);
            if (user is null)
                return new GenericResultDTO(null, 404);

            ApplicationUserMapper.UpdateUserProfileToApplicationUser(updateUserProfile,user);

            var result = await _userManager.UpdateAsync(user);
            if (result.Succeeded)
                return new GenericResultDTO(null, 200);

            return new GenericResultDTO(null, 400);
        }

        public async Task<GenericResultDTO> GetUserInformationByRefreshToken(string refreshToken)
        {
            var token = await _tokenService.GetTokenByRefreshTokenAsync(refreshToken);
            if (token is null)
                return new GenericResultDTO(null, 404);

            var applicationUser = await _userManager.FindByIdAsync(token.ApplicationUserId);
            if (applicationUser is null)
                return new GenericResultDTO(null, 404);

            var roles = await _userManager.GetRolesAsync(applicationUser);

            var userProfileDTO = new GetUserProfileQueryDTO(applicationUser.FirstName, applicationUser.LastName, applicationUser?.Email, applicationUser?.UserName, applicationUser?.PhoneNumber, roles);
            return new GenericResultDTO(userProfileDTO, 200);

        }

        public Task<GenericResultDTO> ApplyToRoleSeller(string applicationUserId)
        {
            throw new NotImplementedException();
        }

       public Task<GenericResultDTO> IsInRoleAsync(string refreshToken)
        {
            throw new NotImplementedException();
        }
    }
}
