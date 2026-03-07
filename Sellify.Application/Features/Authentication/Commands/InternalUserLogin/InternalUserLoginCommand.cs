using MediatR;
using Sellify.Application.Global;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sellify.Application.Features.Authentication.Commands.InternalUserLogin
{
    public class InternalUserLoginCommand :IRequest<GenericResultDTO>
    {
        public InternalUserLoginDTO userLoginDTO { get; init; }
    }
}
