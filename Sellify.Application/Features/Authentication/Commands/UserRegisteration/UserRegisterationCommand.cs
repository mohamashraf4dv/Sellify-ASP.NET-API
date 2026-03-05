using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sellify.Application.Features.Authentication.Commands.UserRegisteration
{
    public class UserRegisterationCommand: IRequest<bool>
    {
        public UserRegisterationDTO userRegisteration { get; init; }
    }
}
