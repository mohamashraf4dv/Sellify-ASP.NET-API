
using Sellify.Application.Features.Token;

namespace Sellify.Application.Features.Authentication.Commands.InternalUserLogin
{
    public class InternalUserLoginCommand :IRequest<GenericResultDTO<TokensDTO>>
    {
        public InternalUserLoginDTO userLoginDTO { get; init; }
    }
}
