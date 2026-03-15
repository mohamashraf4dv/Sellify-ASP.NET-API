
using Sellify.Application.Contracts.Services;
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

        public JwtTokenService(UserManager<ApplicationUser> userManager, IConfiguration configuration,UserHelper userHelper)
        {
            this._userManager = userManager;
            this._configuration = configuration;
            this._userHelper = userHelper;
        }
        public async Task<string> CreateJwtToken(string loginIdentifer, bool isPersistence = false)
        {
            ApplicationUser applicationUser = await _userHelper.GetUserByLoginIdentifer(loginIdentifer);
            if (applicationUser is null)
                return null;

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

            string jwtSecurityTokenHandler = new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken);
            return jwtSecurityTokenHandler;
        }

        private async Task<IEnumerable<Claim>> GetClaims(ApplicationUser applicationUser,bool isPersistence)
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

        
    }
}
