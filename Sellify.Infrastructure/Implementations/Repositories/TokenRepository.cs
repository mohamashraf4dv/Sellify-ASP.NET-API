using Sellify.Application.Features.Token.Query.GetToken;

namespace Sellify.Infrastructure.Implementations.Repositories
{
    internal class TokenRepository :ITokenRepository
    {
        private readonly SellifyMicrosoftSqlContext _context;

        public TokenRepository(SellifyMicrosoftSqlContext context) 
        {
            this._context = context;
        }

        public async Task<bool> AreTokensValid(TokensDTO tokensDTO, CancellationToken cancellationToken = default)
        {
          var token = await  _context.Tokens.SingleOrDefaultAsync(t=> t.RefreshToken == tokensDTO.RefreshToken && t.AccessToken == tokensDTO.AccessToken &&  !t.IsRevoked && !t.IsExpired);
            return (token is null)? false : true;
        }

        public async Task<Token> CreateAsync(Token entity, CancellationToken cancellationToken=default)
        {
           var state = await _context.Tokens.AddAsync(entity, cancellationToken);

            return (state.State == EntityState.Added)? entity: null;
        }
        public async Task<Token> GetTokenByUserIdAsync(string userId, CancellationToken cancellationToken = default)
        {
            return await _context.Tokens.FirstOrDefaultAsync(t=> t.ApplicationUserId== userId, cancellationToken);
        }
    }
}
