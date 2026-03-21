using Sellify.Application.Features.Authentication.Query.GetUserProfile;
using Sellify.Application.Global;

namespace Sellify.Infrastructure.Implementations.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ITokenService _tokenService;

        public UserRepository(UserManager<ApplicationUser> userManager, ITokenService tokenService)
        {
            this._userManager = userManager;
            this._tokenService = tokenService;
        }
        public async Task<GenericResultDTO> GetUserInformationByRefreshToken(string refreshToken)
        {
            var token = await _tokenService.GetTokenByRefreshTokenAsync(refreshToken);
            if(token is null)
                return new GenericResultDTO(null , 404 );
            var applicationUser = await _userManager.FindByIdAsync(token.ApplicationUserId);
            if (applicationUser is null)
                return new GenericResultDTO(null, 404);
            var roles = await _userManager.GetRolesAsync(applicationUser);

            var userProfileDTO = new GetUserProfileQueryDTO(applicationUser.FirstName, applicationUser.LastName, applicationUser?.Email,applicationUser?.UserName, applicationUser?.PhoneNumber, roles);
            return new GenericResultDTO(userProfileDTO, 200);

        }
    }
}
