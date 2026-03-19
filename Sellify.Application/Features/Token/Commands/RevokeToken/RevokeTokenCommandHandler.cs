namespace Sellify.Application.Features.Token.Commands.RevokeToken
{
    public class RevokeTokenCommandHandler : IRequestHandler<RevokeTokenCommand, GenericResultDTO>
    {
        private readonly IJwtTokenService _tokenService;

        public RevokeTokenCommandHandler(IJwtTokenService tokenService)
        {
            this._tokenService = tokenService;
        }
        public async Task<GenericResultDTO> Handle(RevokeTokenCommand request, CancellationToken cancellationToken)
        {
           return await _tokenService.RevokeToken(request.RefreshToken);
        }
    }
}
