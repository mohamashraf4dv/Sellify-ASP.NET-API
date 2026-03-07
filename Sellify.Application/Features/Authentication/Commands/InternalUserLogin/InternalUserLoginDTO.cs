using System;
using System.Collections.Generic;
using System.Text;

namespace Sellify.Application.Features.Authentication.Commands.InternalUserLogin
{
    public record InternalUserLoginDTO
    {
        public string UserName { get; init; }
        public string Password { get; init; }
    }
}
