namespace Sellify.Application.Features.Token.Query.GetToken
{
    public record GetTokensQuery(string RefreshToken, string AccessToken) : IRequest<GenericResultDTO>;

}
