
using Microsoft.Extensions.Logging;
using Sellify.Application.Features.Authentication.Commands.UpdateUserProfile;
using Sellify.Application.Features.Authentication.Query.GetUserProfile;
using Sellify.Application.Global;
using Sellify.Domain.Enums;
using Sellify.Infrastructure.Mapperly;

namespace Sellify.Infrastructure.Services
{
    public class UserService : IUserService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ITokenService _tokenService;
        private readonly ILogger<UserService> _logger;

        public UserService(UserManager<ApplicationUser> userManager , ITokenService tokenService, ILogger<UserService> logger)
        {
            this._userManager = userManager;
            this._tokenService = tokenService;
            this._logger = logger;
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

        public async Task<GenericResultDTO<GetUserProfileQueryDTO>> GetUserInformationByRefreshToken(string refreshToken)
        {
            var token = await _tokenService.GetTokenByRefreshTokenAsync(refreshToken);
            if (token is null)
                return new GenericResultDTO<GetUserProfileQueryDTO>(null, 404);

            var applicationUser = await _userManager.FindByIdAsync(token.ApplicationUserId);
            if (applicationUser is null)
                return new GenericResultDTO<GetUserProfileQueryDTO>(null, 404);

            var roles = await _userManager.GetRolesAsync(applicationUser);

            var userProfileDTO = new GetUserProfileQueryDTO(applicationUser.FirstName, applicationUser.LastName, applicationUser?.Email, applicationUser?.UserName, applicationUser?.PhoneNumber, roles,applicationUser?.SellerRoleRequestStatus.ToString());
            return new GenericResultDTO<GetUserProfileQueryDTO>(userProfileDTO, 200);
        }

        public async Task<GenericResultDTO> RequestRoleAsync(string refreshToken, SellerRoleRequestStatus? sellerRoleRequestStatus = null)
        {
            var token = await _tokenService.GetTokenByRefreshTokenAsync(refreshToken);
            if (token is null)
                return new GenericResultDTO(null, 404);

            var applicationUser = await _userManager.FindByIdAsync(token.ApplicationUserId);
            if (applicationUser is null)
                return new GenericResultDTO(null, 404);

            string errorMessage = null;
            switch (sellerRoleRequestStatus)
            {
                case null:
                    var result = await _userManager.AddToRoleAsync(applicationUser, "Seller");
                    if (result.Succeeded)
                    {
                        applicationUser.SellerRoleRequestStatus = SellerRoleRequestStatus.Approved;
                        applicationUser.Seller= new Seller { Id = applicationUser.Id, IsActive=true };
                    }
                    else
                    {
                        applicationUser.SellerRoleRequestStatus = SellerRoleRequestStatus.Rejected;
                        errorMessage = result.Errors.FirstOrDefault()?.Description;
                        goto default;
                    }
                    break;

                    /* ---- might be added in the future but in another action as admin won't use the same action -> should use the user id --------
                     --- this one works on refreshToken that is sent in http only cookie ---

                case SellerRoleRequestStatus.Approved:

                    //var result = await _userManager.AddToRoleAsync(applicationUser, "Seller");
                    if(result.Succeeded)
                        applicationUser.SellerRoleRequestStatus = SellerRoleRequestStatus.Approved;

                    else {
                        applicationUser.SellerRoleRequestStatus = SellerRoleRequestStatus.Rejected;
                          }

                    break;

                case SellerRoleRequestStatus.Rejected:
                    applicationUser.SellerRoleRequestStatus = SellerRoleRequestStatus.Rejected;
                    break;
                    */
                default:
                    #region Error Initializing
                    var errors = new Dictionary<string, HashSet<string>>();
                    var errorsHashset = new HashSet<string>();
                    errorsHashset.Add("Unexpected Error Happened");
                    errorsHashset.Add(errorMessage);
                    errors.Add("Error", errorsHashset); 
                    #endregion
                    _logger.LogError("Unexpected Error happened when request status is {sellerRoleRequestStatus}", sellerRoleRequestStatus);
                    return new GenericResultDTO(null, 400, errors);
            }
            var identityResult = await _userManager.UpdateAsync(applicationUser);
            if(identityResult.Succeeded)
                return new GenericResultDTO(null, 200);

            return new GenericResultDTO(null, 500);
        }

        public async Task<GenericResultDTO<GetUserProfileQueryDTO>> GetUserInformationById(string userId)
        {
            var applicationUser = await _userManager.FindByIdAsync(userId);
            if (applicationUser is null)
                return new GenericResultDTO<GetUserProfileQueryDTO>(null, 404);

            var roles = await _userManager.GetRolesAsync(applicationUser);

            var userProfileDTO = new GetUserProfileQueryDTO(applicationUser.FirstName, applicationUser.LastName, applicationUser.Email!, applicationUser.UserName!, applicationUser?.PhoneNumber, roles, applicationUser?.SellerRoleRequestStatus.ToString());
            return new GenericResultDTO<GetUserProfileQueryDTO>(userProfileDTO, 200);
        }
    }
}
