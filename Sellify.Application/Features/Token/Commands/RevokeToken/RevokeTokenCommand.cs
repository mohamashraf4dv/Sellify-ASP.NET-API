namespace Sellify.Application.Features.Token.Commands.RevokeToken
{
    public record RevokeTokenCommand(string RefreshToken) : IRequest<GenericResultDTO>;

}
