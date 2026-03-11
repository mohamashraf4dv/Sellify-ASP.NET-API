namespace Sellify.Application.Features.Authentication.Commands.UserRegisteration
{
    public class UserRegisterationCommand: IRequest<GenericResultDTO>
    {
        public UserRegisterationDTO userRegisteration { get; init; }
    }
}
