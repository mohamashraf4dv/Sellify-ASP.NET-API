using Sellify.Application.Features.Authentication.Commands.InternalUserLogin;
using Sellify.Application.Features.Authentication.Commands.UserRegisteration;
using Sellify.Application.Global;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sellify.Application.Contracts
{
    public interface IAuthenticationRepository
    {
        Task<GenericResultDTO> Register(UserRegisterationDTO user);
        Task<GenericResultDTO> InternalLogin(InternalUserLoginDTO userLoginDTO);

    }
}
