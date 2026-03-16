
using Sellify.Application.Contracts.Repositories;
using Sellify.Application.Features.Token.Query.GetToken;

namespace Sellify.Application.Contracts.Services
{
    public interface IJwtTokenService
    {
        public Task<string> CreateJwtToken(string loginIdentifer, bool isPersistence = false);
        public Task<TokensDTO> GenerateTokens(string loginIdentifier, bool isPersistence = false);
        public  Task<TokensDTO> UpdateExistingAccessToken(string refreshToken);
        public Task<bool> ValidateTokens(TokensDTO tokensDTO, bool validateLifeTime = false);
        public Task<GenericResultDTO> RevokeToken(string refreshToken);
    }
}
