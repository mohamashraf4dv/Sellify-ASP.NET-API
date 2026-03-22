namespace Sellify.Application.Features.Authentication.Commands.UpdateUserProfile
{
    public record UpdateUserProfileDTO(string FirstName, string LastName, string Email, string PhoneNumber,string UserName);

}
