
using Sellify.Application.Contracts.Repositories;

namespace Sellify.Application.Features.Authentication.Query.GetUserProfile
{
    public class GetUserProfileQueryHandler : IRequestHandler<GetUserProfileQuery, GenericResultDTO>
    {
        private readonly IUserRepository _userRepository;

        public GetUserProfileQueryHandler(IUserRepository userRepository)
        {
            this._userRepository = userRepository;
        }
        public async Task<GenericResultDTO> Handle(GetUserProfileQuery request, CancellationToken cancellationToken)
        {
            return await _userRepository.GetUserInformationByRefreshToken(request.refreshToken);
        }
    }
}
