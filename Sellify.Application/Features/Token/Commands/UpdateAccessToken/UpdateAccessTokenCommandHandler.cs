namespace Sellify.Application.Features.Token.Commands.UpdateAccessToken
{
    public class UpdateAccessTokenCommandHandler : IRequestHandler<UpdateAccessTokenCommand, GenericResultDTO<TokensDTO>>
    {
        private readonly IJwtTokenService _tokenService;

        public UpdateAccessTokenCommandHandler(IJwtTokenService tokenService)
        {
            this._tokenService = tokenService;
        }
        public async Task<GenericResultDTO<TokensDTO>> Handle(UpdateAccessTokenCommand request, CancellationToken cancellationToken)
        {
            TokensDTO tokens = await _tokenService.UpdateExistingAccessToken(request.refreshToken);
            if (tokens == null)
            {
                var errors = new Dictionary<string, HashSet<string>>();
                errors.Add("Token", new HashSet<string>() { "Bad Request" });
                return new GenericResultDTO<TokensDTO>(null, 400, errors);
            }
            return new GenericResultDTO<TokensDTO>(tokens, 200);
        }
    }
}
