
using Sellify.Application.Features.Authentication.Commands.UpdateUserProfile;
using Sellify.Application.Features.Authentication.Query.GetUserProfile;
using Sellify.Domain.Enums;

namespace Sellify.Application.Contracts.Services
{
    public interface IUserService
    {
        public Task<GenericResultDTO> RequestRoleAsync(string refreshToken ,SellerRoleRequestStatus? sellerRoleRequestStatus=null);
        public Task<GenericResultDTO<GetUserProfileQueryDTO>> GetUserInformationByRefreshToken(string refreshToken);
        public Task<GenericResultDTO> UpdateUserProfile(UpdateUserProfileDTO updateUserProfile);
    }
}
