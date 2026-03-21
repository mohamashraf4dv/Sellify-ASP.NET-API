using Sellify.Application.Features.Token;

namespace Sellify.Application.Contracts.Repositories
{
    public interface ITokenRepository
    {
        public Task<Token> CreateAsync(Token entity, CancellationToken cancellationToken = default);
        public Task<Token> GetTokenByUserIdAsync(string userId, CancellationToken cancellationToken=default);
        public Task<Token> GetTokenByRefreshTokenAsync(string refreshToken, CancellationToken cancellationToken=default);
        public Task<bool> AreTokensExistInDbAsync(TokensDTO tokensDTO, CancellationToken cancellationToken=default);
    }
}
