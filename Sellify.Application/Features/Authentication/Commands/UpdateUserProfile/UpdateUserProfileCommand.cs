namespace Sellify.Application.Features.Authentication.Commands.UpdateUserProfile
{
    public record UpdateUserProfileCommand(UpdateUserProfileDTO UpdateUserProfileDTO) : IRequest<GenericResultDTO>;

}
