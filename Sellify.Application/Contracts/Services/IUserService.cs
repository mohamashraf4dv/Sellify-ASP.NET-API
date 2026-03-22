
using Sellify.Application.Features.Authentication.Commands.UpdateUserProfile;

namespace Sellify.Application.Contracts.Services
{
    public interface IUserService
    {
        public Task<GenericResultDTO> ApplyToRoleSeller(string applicationUserId);
        public Task<GenericResultDTO> IsInRoleAsync(string refreshToken);
        public Task<GenericResultDTO> GetUserInformationByRefreshToken(string refreshToken);
        public Task<GenericResultDTO> UpdateUserProfile(UpdateUserProfileDTO updateUserProfile);
    }
}
