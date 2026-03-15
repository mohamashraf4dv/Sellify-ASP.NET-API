
using Sellify.Application.Contracts.Services;
using Sellify.Application.Features.Token.Query.GetToken;
using Sellify.Infrastructure.ServicesHelper;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace Sellify.Infrastructure.Services
{
    public class JwtTokenService:IJwtTokenService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IConfiguration _configuration;
        private readonly UserHelper _userHelper;
        private readonly ITokenRepository _tokenRepository;

        public JwtTokenService(UserManager<ApplicationUser> userManager, IConfiguration configuration,UserHelper userHelper,ITokenRepository tokenRepository)
        {
            this._userManager = userManager;
            this._configuration = configuration;
            this._userHelper = userHelper;
            this._tokenRepository = tokenRepository;
        }
        public async Task<string> CreateJwtToken(string loginIdentifier, bool isPersistence = false)
        {
            ApplicationUser applicationUser = await GetApplicationUserAsync(loginIdentifier);

            if (applicationUser is null)
                return null;

            JwtSecurityToken jwtSecurityToken = await GenerateJwtSecurityToken(applicationUser, isPersistence);
            return new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken);

        }
        public async Task<TokensDTO> GenerateTokens(string loginIdentifier)
        {
            ApplicationUser applicationUser = await GetApplicationUserAsync(loginIdentifier);

            if (applicationUser is null )
                return null;


            JwtSecurityToken jwtSecurityToken = await GenerateJwtSecurityToken(applicationUser);
            string jwtToken = new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken);
            return new TokensDTO(Guid.NewGuid().ToString(), jwtToken);
        }
        public async Task<bool> ValidateTokens(TokensDTO tokensDTO)
        {
            if (tokensDTO is null || tokensDTO.AccessToken is null || tokensDTO.RefreshToken is null) return false;

            JwtSecurityTokenHandler jwtSecurityTokenHandler = new JwtSecurityTokenHandler();
            var result = await jwtSecurityTokenHandler.ValidateTokenAsync(tokensDTO.AccessToken, new TokenValidationParameters()
            {
                ValidateIssuer = true,
                ValidIssuer = _configuration["Jwt:Issuer"],
                ValidateAudience = true,
                ValidAudience = _configuration["Jwt:Audience"],
                ValidateLifetime = true,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:SecretKey"])),
            });
            if (!result.IsValid)
                return false;

            bool isValidInDb = await _tokenRepository.AreTokensValid(tokensDTO);
            return isValidInDb;
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


    }
}
