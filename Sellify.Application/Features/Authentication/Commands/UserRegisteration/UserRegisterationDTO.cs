using System;
using System.Collections.Generic;
using System.Text;

namespace Sellify.Application.Features.Authentication.Commands.UserRegisteration
{
    public record UserRegisterationDTO(
        string FirstName,
        string LastName,
        string UserName,
        string Email,
        string Password,
        DateOnly DateOfBirth,
        string? PhoneNumber
    );
}
