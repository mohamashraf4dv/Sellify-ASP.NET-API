using Sellify.Application.Features.Token;
using Sellify.Application.Global.Results;

namespace Sellify.Application.Contracts.Services
{
    public interface ITokenService
    {
        public Task<string> CreateJwtToken(string loginIdentifer, bool isPersistence = false);
        public Task<TokensDTO> GenerateTokens(string loginIdentifier, bool isPersistence = false);
        public  Task<TokensDTO> UpdateExistingAccessToken(string refreshToken);
        public Task<bool> ValidateTokens(TokensDTO tokensDTO, bool validateLifeTime = false);
        public Task<Result> RevokeToken(string refreshToken);
        public Task<Token> GetTokenByRefreshTokenAsync(string refreshToken, CancellationToken cancellationToken = default);

    }
}
