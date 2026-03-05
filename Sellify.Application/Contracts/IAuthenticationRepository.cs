using Sellify.Application.Features.Authentication.Commands.UserRegisteration;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sellify.Application.Contracts
{
    public interface IAuthenticationRepository
    {
        Task<bool> Register(UserRegisterationDTO user);
        Task<string> Login(string userName, string password);

    }
}
