using MediatR;
using Sellify.Application.Contracts;
using Sellify.Application.Global;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sellify.Application.Features.Authentication.Commands.InternalUserLogin
{
    public class InternalUserLoginCommandHandler : IRequestHandler<InternalUserLoginCommand, GenericResultDTO>
    {
        private readonly IAuthenticationRepository _authenticationRepository;

        public InternalUserLoginCommandHandler(IAuthenticationRepository authenticationRepository)
        {
            this._authenticationRepository = authenticationRepository;
        }
        public async Task<GenericResultDTO> Handle(InternalUserLoginCommand request, CancellationToken cancellationToken)
        {
            var result = await this._authenticationRepository.InternalLogin(request.userLoginDTO);
            return result;
        }
    }
}
