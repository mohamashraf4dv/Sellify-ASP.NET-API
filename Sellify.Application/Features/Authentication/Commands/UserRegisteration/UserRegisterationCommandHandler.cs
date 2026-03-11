
namespace Sellify.Application.Features.Authentication.Commands.UserRegisteration
{
    public class UserRegisterationCommandHandler : IRequestHandler<UserRegisterationCommand, GenericResultDTO>
    {
        private readonly IAuthenticationService _authenticationService;
        public UserRegisterationCommandHandler(IAuthenticationService authenticationService)
        {
            this._authenticationService = authenticationService;
        }
        public async Task<GenericResultDTO> Handle(UserRegisterationCommand request, CancellationToken cancellationToken)
        {
            GenericResultDTO result = await _authenticationService.Register(request.userRegisteration);
            return result;
        }
    
    }
}
