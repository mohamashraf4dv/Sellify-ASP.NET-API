
using Sellify.Application.Contracts.Repositories;

namespace Sellify.Application.Features.Authentication.Query.GetUserProfile
{
    public class GetUserProfileQueryHandler : IRequestHandler<GetUserProfileQuery, GenericResultDTO<GetUserProfileQueryDTO>>
    {
        private readonly IUserService _userService;

        public GetUserProfileQueryHandler(IUserService userService)
        {
            this._userService = userService;
        }
        public async Task<GenericResultDTO<GetUserProfileQueryDTO>> Handle(GetUserProfileQuery request, CancellationToken cancellationToken)
        {
            return await _userService.GetUserInformationByRefreshToken(request.refreshToken);
        }
    }
}
