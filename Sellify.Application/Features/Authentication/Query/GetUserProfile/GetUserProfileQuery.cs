namespace Sellify.Application.Features.Authentication.Query.GetUserProfile
{
    public record GetUserProfileQuery(string userId) : IRequest<GenericResultDTO<GetUserProfileQueryDTO>>;
}
