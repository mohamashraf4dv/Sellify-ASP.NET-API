using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using Sellify.Application.Contracts;
using Sellify.Application.Features.Authentication.Commands.InternalUserLogin;
using Sellify.Application.Features.Authentication.Commands.UserRegisteration;
using Sellify.Application.Global;
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
        private readonly ILogger<AuthenticationRepository> _logger;

        public AuthenticationRepository(SellifyMicrosoftSqlContext context,UserManager<ApplicationUser> userManager,IConfiguration configuration, ILogger<AuthenticationRepository> logger) 
        {
            this._context = context;
            this._userManager = userManager;
            this._configuration = configuration;
            _logger = logger;
        }
        public async Task<GenericResultDTO> Register(UserRegisterationDTO user) { 
        
          IdentityResult userCreationResult = await _userManager.CreateAsync(new ApplicationUser
            {
                FirstName = user?.FirstName,
                LastName = user?.LastName,
                UserName = user?.UserName,
                Email = user?.Email,
                DateOfBirth = user.DateOfBirth,
                PhoneNumber = user?.PhoneNumber
            },user?.Password);

            _logger.LogWarning("User {UserName} registration attempt resulted in {Result}", user.UserName, userCreationResult.Succeeded ? "success" : "failure");
            var errors = new Dictionary<string, HashSet<string>>();
            foreach(IdentityError error in userCreationResult.Errors)
            {
                if (!errors.ContainsKey(error.Code))
                {
                    errors[error.Code] = new HashSet<string>();
                }
                errors[error.Code].Add(error.Description);
            }
            int statusCode = userCreationResult.Succeeded ? StatusCodes.Status201Created : StatusCodes.Status400BadRequest;
            if (userCreationResult.Succeeded)
            {
                _logger.LogInformation("User {UserName} registered successfully.", user.UserName);
                string jwtToken = await CreateJwtToken(user.UserName);
            return new GenericResultDTO(new { Token= jwtToken },statusCode );

            }
            return new GenericResultDTO(new { userCreationResult },statusCode,errorsKeyValues: errors );
        }
        public async Task<GenericResultDTO> InternalLogin(InternalUserLoginDTO userLoginDTO) 
        {
            ApplicationUser? user = await _userManager.Users.FirstOrDefaultAsync(u => u.UserName == userLoginDTO.UserName);
            if (user == null)
            {
                _logger.LogInformation("User {UserName} tried to login but is not in our database", userLoginDTO.UserName);

                return new GenericResultDTO(data: null, statusCode: StatusCodes.Status404NotFound, errorsKeyValues: new Dictionary<string, HashSet<string>> { { "UserName", new HashSet<string> { $"No user found with username '{userLoginDTO.UserName}'." } } });
            }
            bool isPasswordValid = _userManager.CheckPasswordAsync(user, userLoginDTO.Password).Result;

            if (!isPasswordValid)
            {
                _logger.LogInformation("User {UserName} tried to login with invalid password.", userLoginDTO.UserName);

                return new GenericResultDTO(data:null,statusCode: StatusCodes.Status400BadRequest,errorsKeyValues: new Dictionary<string, HashSet<string>> { { "Password", new HashSet<string> { "Invalid password." } } });
            }
            string jwtToken = await CreateJwtToken(userLoginDTO.UserName);
            _logger.LogInformation("User {UserName} logged in successfully.", userLoginDTO.UserName);
            return new GenericResultDTO(data: new { Token = jwtToken }, statusCode: StatusCodes.Status202Accepted);

        }
        private Task<string> CreateJwtToken(string userName) 
        {
            DateTime expiryDate = DateTime.UtcNow.AddMinutes(5);
            IEnumerable<Claim> userClaims = new List<Claim>
            {
                new Claim("JWTID",Guid.NewGuid().ToString()),
                new Claim(ClaimTypes.Name,userName)
            };
            string issuer = _configuration["JWT:Issuer"];
            string audience = _configuration["JWT:Audience"];
            SecurityKey securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:SecretKey"]));
            SigningCredentials signingCredentials = new SigningCredentials(securityKey,SecurityAlgorithms.HmacSha256);
            JwtSecurityToken jwtSecurityToken = new JwtSecurityToken
                (claims:userClaims,
                signingCredentials:signingCredentials,
                expires:expiryDate,
                issuer:issuer,
                audience:audience);

            string jwtSecurityTokenHandler = new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken);
            return Task.FromResult(jwtSecurityTokenHandler); 
        }
    }
}
