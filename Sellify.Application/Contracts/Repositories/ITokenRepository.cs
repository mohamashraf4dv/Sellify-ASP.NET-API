using Sellify.Application.Features.Token.Query.GetToken;

namespace Sellify.Application.Contracts.Repositories
{
    public interface ITokenRepository
    {
        public Task<Token> CreateAsync(Token entity, CancellationToken cancellationToken = default);
        public Task<Token> GetTokenByUserIdAsync(string userId, CancellationToken cancellationToken=default);
        public Task<bool> AreTokensValid(TokensDTO tokensDTO, CancellationToken cancellationToken=default);
        
    }
}
