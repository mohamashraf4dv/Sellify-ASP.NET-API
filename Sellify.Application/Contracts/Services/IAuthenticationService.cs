

using Sellify.Application.Features.Token;

namespace Sellify.Application.Contracts.Services
{
    public interface IAuthenticationService
    {
        public Task<GenericResultDTO<TokensDTO>> Register(UserRegisterationDTO user);
        public  Task<GenericResultDTO<TokensDTO>> InternalLogin(InternalUserLoginDTO userLoginDTO);
    }
}
