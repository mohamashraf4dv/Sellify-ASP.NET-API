
namespace Sellify.Application.Features.Authentication.Commands.InternalUserLogin
{
    public class InternalUserLoginCommand :IRequest<GenericResultDTO>
    {
        public InternalUserLoginDTO userLoginDTO { get; init; }
    }
}
