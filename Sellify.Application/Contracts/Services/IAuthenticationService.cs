
using Sellify.Application.Features.Token.Query.GetToken;

namespace Sellify.Application.Contracts.Services
{
    public interface IAuthenticationService
    {
        public Task<GenericResultDTO<TokensDTO>> Register(UserRegisterationDTO user);
        public  Task<GenericResultDTO> InternalLogin(InternalUserLoginDTO userLoginDTO);
    }
}
