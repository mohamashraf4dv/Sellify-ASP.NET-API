
namespace Sellify.Application.Contracts.Services
{
    public interface IAuthenticationService
    {
        public Task<GenericResultDTO> Register(UserRegisterationDTO user);
        public  Task<GenericResultDTO> InternalLogin(InternalUserLoginDTO userLoginDTO);
    }
}
