using Sellify.Application.Features.Token;

namespace Sellify.Application.Features.Authentication.Commands.InternalUserLogin
{
    public class InternalUserLoginCommandHandler : IRequestHandler<InternalUserLoginCommand, GenericResultDTO<TokensDTO>>
    {
        private readonly IAuthenticationService _authenticationService;

        public InternalUserLoginCommandHandler(IAuthenticationService authenticationService)
        {
            this._authenticationService = authenticationService;
        }
        public async Task<GenericResultDTO<TokensDTO>> Handle(InternalUserLoginCommand request, CancellationToken cancellationToken)
        {
            var result = await this._authenticationService.InternalLogin(request.userLoginDTO);
            return result;
        }
    }
}
