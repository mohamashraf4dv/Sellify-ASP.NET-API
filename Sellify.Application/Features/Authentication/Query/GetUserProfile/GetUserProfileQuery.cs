namespace Sellify.Application.Features.Authentication.Query.GetUserProfile
{
    public record GetUserProfileQuery(string refreshToken) : IRequest<GenericResultDTO>;

}
