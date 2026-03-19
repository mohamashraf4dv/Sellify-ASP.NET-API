

using Sellify.Application.Features.Token;

namespace Sellify.Application.Features.Authentication.Commands.UserRegisteration
{
    public class UserRegisterationCommandHandler : IRequestHandler<UserRegisterationCommand, GenericResultDTO<TokensDTO>>
    {
        private readonly IAuthenticationService _authenticationService;
        public UserRegisterationCommandHandler(IAuthenticationService authenticationService)
        {
            this._authenticationService = authenticationService;
        }
        public async Task<GenericResultDTO<TokensDTO>> Handle(UserRegisterationCommand request, CancellationToken cancellationToken)
        {
            GenericResultDTO<TokensDTO> result = await _authenticationService.Register(request.userRegisteration);
            return result;
        }
    
    }
}
