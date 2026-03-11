
namespace Sellify.Application.Contracts.Services
{
    public interface IJwtTokenService
    {
        public Task<string> CreateJwtToken(string loginIdentifer, bool isPersistence = false);
    }
}
