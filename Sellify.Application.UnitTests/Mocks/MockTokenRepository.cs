using Moq;
using Sellify.Application.Contracts.Repositories;
using Sellify.Application.Features.Token;
using Sellify.Domain.Entities;

namespace Sellify.Application.UnitTests.Mocks
{
    public class MockTokenRepository
    {
        public static Mock<ITokenRepository> GetMockTokenRepository()
        {
            var tokens = new List<Token>()
            {
                new Token()
                {
                    ApplicationUserId="1",
                    AccessToken= "AccessToken1",
                    RefreshToken= "RefreshToken1",
                    ExpirationDate=DateTime.Now.AddDays(1)
                },
                new Token()
                {
                    ApplicationUserId="2",
                    AccessToken= "AccessTokenExpired",
                    RefreshToken= "RefreshTokenExpired",
                    ExpirationDate=DateTime.Now.AddDays(-2)
                },
            };

            var mockRepository = new Mock<ITokenRepository>();

            mockRepository.Setup(repo => repo.CreateAsync(It.IsAny<Token>()))
                .Returns((Token token) =>
            {
                tokens.Add(token);
                return token;
            });

            mockRepository.Setup(repo => repo.AreTokensExistInDbAsync(new TokensDTO("RefreshToken1", "AccessToken1") ))
                .Returns((TokensDTO td)=> tokens.SingleOrDefault(t=> t.RefreshToken== td.RefreshToken && t.AccessToken== td.AccessToken));

            mockRepository.Setup(repo=> repo.GetTokenByUserIdAsync("1"))
                .Returns((string userId)=> tokens.SingleOrDefault(t=> t.ApplicationUserId == userId));

            mockRepository.Setup(repo => repo.GetTokenByRefreshTokenAsync("RefreshToken1"))
                .Returns((string refreshTokenString) => tokens.SingleOrDefault(t => t.RefreshToken == refreshTokenString));


            return mockRepository;
        }
    }
}
