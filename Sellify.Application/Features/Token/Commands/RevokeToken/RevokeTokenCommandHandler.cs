using Sellify.Application.Global.Results;

namespace Sellify.Application.Features.Token.Commands.RevokeToken
{
    public class RevokeTokenCommandHandler : IRequestHandler<RevokeTokenCommand, Result>
    {
        private readonly ITokenService _tokenService;

        public RevokeTokenCommandHandler(ITokenService tokenService)
        {
            this._tokenService = tokenService;
        }
        public async Task<Result> Handle(RevokeTokenCommand request, CancellationToken cancellationToken)
        {
           return await _tokenService.RevokeToken(request.RefreshToken);
        }
    }
}
