using MediatR;
using Sellify.Application.Global;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sellify.Application.Features.Authentication.Commands.UserRegisteration
{
    public class UserRegisterationCommand: IRequest<GenericResultDTO>
    {
        public UserRegisterationDTO userRegisteration { get; init; }
    }
}
