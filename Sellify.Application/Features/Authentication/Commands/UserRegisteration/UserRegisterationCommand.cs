using Sellify.Application.Features.Token;
namespace Sellify.Application.Features.Authentication.Commands.UserRegisteration
{
    public class UserRegisterationCommand: IRequest<GenericResultDTO<TokensDTO>>
    {
        public UserRegisterationDTO userRegisteration { get; init; }
    }
}
