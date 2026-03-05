using MediatR;
using Sellify.Application.Contracts;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sellify.Application.Features.Authentication.Commands.UserRegisteration
{
    public class UserRegisterationCommandHandler : IRequestHandler<UserRegisterationCommand, bool>
    {
        private readonly IAuthenticationRepository _authenticationRepository;
        public UserRegisterationCommandHandler(IAuthenticationRepository authenticationRepository)
        {
            this._authenticationRepository = authenticationRepository;
        }
        public async Task<bool> Handle(UserRegisterationCommand request, CancellationToken cancellationToken)
        {
            bool isRegisteredSuccessfully = await _authenticationRepository.Register(request.userRegisteration);
            return isRegisteredSuccessfully;
        }
    
    }
}
