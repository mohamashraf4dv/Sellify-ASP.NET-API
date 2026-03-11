namespace Sellify.Application.Features.Authentication.Commands.InternalUserLogin
{
    public class InternalUserLoginCommandHandler : IRequestHandler<InternalUserLoginCommand, GenericResultDTO>
    {
        private readonly IAuthenticationService _authenticationService;

        public InternalUserLoginCommandHandler(IAuthenticationService authenticationService)
        {
            this._authenticationService = authenticationService;
        }
        public async Task<GenericResultDTO> Handle(InternalUserLoginCommand request, CancellationToken cancellationToken)
        {
            var result = await this._authenticationService.InternalLogin(request.userLoginDTO);
            return result;
        }
    }
}
