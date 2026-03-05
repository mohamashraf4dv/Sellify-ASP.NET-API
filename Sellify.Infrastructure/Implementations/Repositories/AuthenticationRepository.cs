using Microsoft.IdentityModel.Tokens;
using Sellify.Application.Contracts;
using Sellify.Application.Features.Authentication.Commands.UserRegisteration;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Sellify.Infrastructure.Implementations.Repositories
{
    public class AuthenticationRepository:IAuthenticationRepository
    {
        private readonly SellifyMicrosoftSqlContext _context;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IConfiguration _configuration;

        public AuthenticationRepository(SellifyMicrosoftSqlContext context,UserManager<ApplicationUser> userManager,IConfiguration configuration) 
        {
            this._context = context;
            this._userManager = userManager;
            this._configuration = configuration;
        }
        public async Task<bool> Register(UserRegisterationDTO user) { 
        
          IdentityResult userCreationResult = await _userManager.CreateAsync(new ApplicationUser
            {
                FirstName = user.FirstName,
                LastName = user.LastName,
                UserName = user.UserName,
                Email = user.Email,
                DateOfBirth = user.DateOfBirth,
                PhoneNumber = user?.PhoneNumber
            },user.Password);

            return userCreationResult.Succeeded;
        }
        public Task<string> Login(string userName, string password) 
        {
            ApplicationUser? user = _userManager.Users.FirstOrDefault(u => u.UserName == userName);
            if (user == null)
            {
                return Task.FromResult<string>(null);
            }
            bool isPasswordValid = _userManager.CheckPasswordAsync(user, password).Result;

            if (!isPasswordValid)
            {
                return Task.FromResult<string>(null);
            }
            return CreateJwtToken(userName);
        }
        private Task<string> CreateJwtToken(string userName) 
        {
            DateTime expiryDate = DateTime.UtcNow.AddMinutes(5);
            IEnumerable<Claim> userClaims = new List<Claim>
            {
                new Claim(ClaimTypes.Name,userName)
            };
            SecurityKey securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:SecretKey"]));
            SigningCredentials signingCredentials = new SigningCredentials(securityKey,SecurityAlgorithms.HmacSha256);
            JwtSecurityToken jwtSecurityToken = new JwtSecurityToken(claims:userClaims,signingCredentials:signingCredentials,expires:expiryDate);

            string jwtSecurityTokenHandler = new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken);
            return Task.FromResult(jwtSecurityTokenHandler); 
        }
    }
}
