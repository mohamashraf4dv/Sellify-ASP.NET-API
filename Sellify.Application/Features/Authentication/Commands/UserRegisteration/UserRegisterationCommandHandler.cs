using MediatR;
using Sellify.Application.Contracts;
using Sellify.Application.Global;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sellify.Application.Features.Authentication.Commands.UserRegisteration
{
    public class UserRegisterationCommandHandler : IRequestHandler<UserRegisterationCommand, GenericResultDTO>
    {
        private readonly IAuthenticationRepository _authenticationRepository;
        public UserRegisterationCommandHandler(IAuthenticationRepository authenticationRepository)
        {
            this._authenticationRepository = authenticationRepository;
        }
        public async Task<GenericResultDTO> Handle(UserRegisterationCommand request, CancellationToken cancellationToken)
        {
            GenericResultDTO result = await _authenticationRepository.Register(request.userRegisteration);
            return result;
        }
    
    }
}
