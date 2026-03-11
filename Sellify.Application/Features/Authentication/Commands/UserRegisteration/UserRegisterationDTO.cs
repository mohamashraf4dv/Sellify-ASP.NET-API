
namespace Sellify.Application.Features.Authentication.Commands.UserRegisteration
{
    public record UserRegisterationDTO(
        string FirstName,
        string LastName,
        string UserName,
        string Email,
        string Password,
        string DateOfBirth,
        string? PhoneNumber = null
    );
}
