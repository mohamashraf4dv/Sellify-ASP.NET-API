using Sellify.Application.Features.Token;
using Sellify.Application.Global;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace Sellify.Infrastructure.Services
{
    public class TokenService:ITokenService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IConfiguration _configuration;
        private readonly UserHelper _userHelper;
        private readonly ITokenRepository _tokenRepository;
        private readonly IUnitOfWork _unitOfWork;

        public TokenService(UserManager<ApplicationUser> userManager, IConfiguration configuration,UserHelper userHelper,ITokenRepository tokenRepository,IUnitOfWork unitOfWork)
        {
            this._userManager = userManager;
            this._configuration = configuration;
            this._userHelper = userHelper;
            this._tokenRepository = tokenRepository;
            this._unitOfWork = unitOfWork;
        }
        public async Task<string> CreateJwtToken(string loginIdentifier, bool isPersistence = false)
        {
            ApplicationUser applicationUser = await GetApplicationUserAsync(loginIdentifier);

            if (applicationUser is null)
                return null;

            JwtSecurityToken jwtSecurityToken = await GenerateJwtSecurityToken(applicationUser, isPersistence);
            return new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken);

        }
        public async Task<TokensDTO> GenerateTokens(string loginIdentifier,bool isPersistence=false)
        {
            ApplicationUser applicationUser = await GetApplicationUserAsync(loginIdentifier);

            if (applicationUser is null )
                return null;

            JwtSecurityToken jwtSecurityToken = await GenerateJwtSecurityToken(applicationUser);
            string jwtToken = new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken);

            DateTime expirationDate = isPersistence ? DateTime.UtcNow.AddDays(30) : DateTime.UtcNow.AddMinutes(10);
            Token existedToken = await _tokenRepository.GetTokenByUserIdAsync(applicationUser.Id);
            string refreshToken = Guid.NewGuid().ToString();

            if (existedToken is null)
            {
                Token token = new Token() { ApplicationUserId = applicationUser.Id, AccessToken = jwtToken,ExpirationDate = expirationDate,RefreshToken= refreshToken};
                await _tokenRepository.CreateAsync(token);
            }
            else
            {
                existedToken.IssuedDate= DateTime.UtcNow;
                existedToken.IsRevoked = false;
                existedToken.ExpirationDate = expirationDate;
                existedToken.AccessToken = jwtToken;
                existedToken.RevokationDate = null;
                existedToken.RefreshToken = refreshToken;
            }

            await _unitOfWork.SaveChangesAsync();
            return new TokensDTO(refreshToken, jwtToken);
        }
        public async Task<TokensDTO> UpdateExistingAccessToken(string refreshToken)
        {
            Token token = await _tokenRepository.GetTokenByRefreshTokenAsync(refreshToken);
            if (token is null)
                return null;

            bool isValid = await ValidateTokens(new TokensDTO(token.RefreshToken,token.AccessToken));
            if (!isValid)
                return null;

            if (token.IsRevoked)
            {
                refreshToken = Guid.NewGuid().ToString();
                token.RevokationDate = null;
                token.IsRevoked = false;
            }

            var applicationUser = await GetApplicationUserAsync(token.ApplicationUserId);
            var jwtSecuritytoken = await GenerateJwtSecurityToken(applicationUser);
            string jwtToken = new JwtSecurityTokenHandler().WriteToken(jwtSecuritytoken);

            token.IssuedDate = DateTime.UtcNow;
            token.ExpirationDate = DateTime.UtcNow.AddDays(30);
            token.AccessToken = jwtToken;
            token.RefreshToken = refreshToken;

            await _unitOfWork.SaveChangesAsync();
            return new TokensDTO(refreshToken, jwtToken);
        }
        public async Task<bool> ValidateTokens(TokensDTO tokensDTO,bool validateLifeTime=false)
        {
            if (tokensDTO is null || tokensDTO.AccessToken is null || tokensDTO.RefreshToken is null) return false;

            JwtSecurityTokenHandler jwtSecurityTokenHandler = new JwtSecurityTokenHandler();
            var result = await jwtSecurityTokenHandler.ValidateTokenAsync(tokensDTO.AccessToken, new TokenValidationParameters()
            {
                ValidateIssuer = true,
                ValidIssuer = _configuration["Jwt:Issuer"],
                ValidateAudience = true,
                ValidAudience = _configuration["Jwt:Audience"],
                ValidateLifetime = validateLifeTime,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:SecretKey"])),
                ClockSkew = TimeSpan.Zero
            });
            if (!result.IsValid)
                return false;

            bool areTokensExists = await _tokenRepository.AreTokensExistInDbAsync(tokensDTO);
            if (!areTokensExists)
                return false;

            Token token = await _tokenRepository.GetTokenByRefreshTokenAsync(tokensDTO.RefreshToken);
            return !token.IsRevoked && !token.IsExpired;
        }
        public async Task<GenericResultDTO> RevokeToken(string refreshToken)
        {
            Token token = await _tokenRepository.GetTokenByRefreshTokenAsync(refreshToken);
            var errors = new Dictionary<string, HashSet<string>>();
            if(token is null)
            {
                errors.Add("Refresh Token", new HashSet<string>() { "Token not found"});
                return new GenericResultDTO(null, 404, errors);
            }
            else if (token.IsRevoked)
            {
                    errors.Add("Refresh Token", new HashSet<string>() { "Token already revoked" });
                    return new GenericResultDTO(null, 400, errors);
            }
            token.IsRevoked = true;
            token.ExpirationDate= DateTime.UtcNow;
            token.RevokationDate = DateTime.UtcNow;
            await _unitOfWork.SaveChangesAsync();
            return new GenericResultDTO( null, 200 );
        }

        private async Task<IEnumerable<Claim>> GetClaims(ApplicationUser applicationUser,bool isPersistence=false)
        {

            var userRoles =  await _userManager.GetRolesAsync(applicationUser);
            var userClaims = await  _userManager.GetClaimsAsync(applicationUser);

            IEnumerable<Claim> claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Jti,Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.Sid, applicationUser.Id),
                new Claim(JwtRegisteredClaimNames.Email,applicationUser.Email),
                new Claim(JwtRegisteredClaimNames.Name,applicationUser.FullName),
                new Claim(JwtRegisteredClaimNames.Picture,applicationUser.ImageURL?? ""),
                new Claim(ClaimTypes.IsPersistent, isPersistence.ToString().ToLower())
            }
            .Union(userRoles.Select(role => new Claim(ClaimTypes.Role, role)))
            .Union(userClaims);

            return claims;

        }
        private async Task<JwtSecurityToken> GenerateJwtSecurityToken(ApplicationUser applicationUser,bool isPersistence=false)
        {
            ///--- Expiration Date ---///
            DateTime expiryDate = DateTime.UtcNow.AddMinutes(5);

            IEnumerable<Claim> claims = await GetClaims(applicationUser, isPersistence);

            #region Issuer & Audience Configurations
            string issuer = _configuration["JWT:Issuer"];
            string audience = _configuration["JWT:Audience"];

            #endregion

            SecurityKey securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:SecretKey"]));

            SigningCredentials signingCredentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            JwtSecurityToken jwtSecurityToken = new JwtSecurityToken
                (claims: claims,
                signingCredentials: signingCredentials,
                expires: expiryDate,
                issuer: issuer,
                audience: audience);

            //string accessToken = new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken);
            return jwtSecurityToken;
        }
        private async Task<ApplicationUser> GetApplicationUserAsync(string loginIdentifier) 
        {
            ApplicationUser applicationUser = await _userHelper.GetUserByLoginIdentifer(loginIdentifier);
            if (applicationUser is null)
            {
                applicationUser = await _userManager.FindByIdAsync(loginIdentifier);

                if (applicationUser is null)
                    return null;
            }

            return applicationUser;
        }

        public async Task<Token> GetTokenByRefreshTokenAsync(string refreshToken, CancellationToken cancellationToken = default)
        {
            return await _tokenRepository.GetTokenByRefreshTokenAsync(refreshToken);
        }
    }
}
