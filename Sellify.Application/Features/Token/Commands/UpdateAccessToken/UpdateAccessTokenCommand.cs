namespace Sellify.Application.Features.Token.Commands.UpdateAccessToken
{
    public record UpdateAccessTokenCommand(string refreshToken) : IRequest<GenericResultDTO<TokensDTO>>;

}
