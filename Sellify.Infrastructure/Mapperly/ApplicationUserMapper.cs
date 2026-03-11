using Riok.Mapperly.Abstractions;
using Sellify.Application.Features.Authentication.Commands.UserRegisteration;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sellify.Infrastructure.Mapperly
{
    [Mapper]
    public static partial class ApplicationUserMapper
    {
        public static partial ApplicationUser UserRegisterationDtoToApplicationUser(UserRegisterationDTO userRegisterationDTO);
    }
}
