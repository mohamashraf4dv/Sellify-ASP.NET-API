namespace Sellify.Infrastructure.Services
{
    public class AuthenticationService:IAuthenticationService
    {
            private readonly UserManager<ApplicationUser> _userManager;
        private readonly ITokenService _jwtTokenService;
            private readonly ILogger<AuthenticationService> _logger;
        private readonly UserHelper _userHelper;

        public AuthenticationService( UserManager<ApplicationUser> userManager, ITokenService jwtTokenService, ILogger<AuthenticationService> logger, UserHelper userHelper)
            {
                this._userManager = userManager;
            this._jwtTokenService = jwtTokenService;
                _logger = logger;
            this._userHelper = userHelper;
        }
        //public async Task<GenericResultDTO> Register(UserRegisterationDTO user)
        //{

        //    ApplicationUser applicationUser = ApplicationUserMapper.UserRegisterationDtoToApplicationUser(user);

        //    IdentityResult userCreationResult = await _userManager.CreateAsync(applicationUser, user.Password);

        //    int statusCode = userCreationResult.Succeeded ? StatusCodes.Status201Created : StatusCodes.Status400BadRequest;

        //    if (!userCreationResult.Succeeded)
        //    {
        //        var errors = GetIdentityErrors(userCreationResult.Errors);
        //        return new GenericResultDTO(new { userCreationResult }, statusCode, errorsKeyValues: errors);

        //    }

        //     string jwtToken = await _jwtTokenService.CreateJwtToken(applicationUser.Email!);
        //     return new GenericResultDTO(new { Token = jwtToken }, statusCode);
        //}
        public async Task<GenericResultDTO<TokensDTO>> Register(UserRegisterationDTO user)
        {

            ApplicationUser applicationUser = ApplicationUserMapper.UserRegisterationDtoToApplicationUser(user);

            IdentityResult userCreationResult = await _userManager.CreateAsync(applicationUser, user.Password);

            int statusCode = userCreationResult.Succeeded ? StatusCodes.Status201Created : StatusCodes.Status400BadRequest;

            if (!userCreationResult.Succeeded)
            {
                var errors = GetIdentityErrors(userCreationResult.Errors);
                return new GenericResultDTO<TokensDTO>(null, statusCode, errorsKeyValues: errors);
            }

            TokensDTO token = await _jwtTokenService.GenerateTokens(applicationUser.Email!);
            return new GenericResultDTO<TokensDTO>(token, statusCode);
        }
        public async Task<GenericResultDTO<TokensDTO>> InternalLogin(InternalUserLoginDTO userLoginDTO)
            {
                ApplicationUser? user = await _userHelper.GetUserByLoginIdentifer(userLoginDTO.LoginIdentifier);

                if (user == null)
                {
                    _logger.LogInformation("User {LoginIdentifier} tried to login but is not in our database", userLoginDTO.LoginIdentifier);

                    return new GenericResultDTO<TokensDTO>(data: null, statusCode: StatusCodes.Status404NotFound, errorsKeyValues: new Dictionary<string, HashSet<string>> { { "Credentials", new HashSet<string> { "invalid credentials" } } });
                }
                bool isPasswordValid = _userManager.CheckPasswordAsync(user, userLoginDTO.Password).Result;

                if (!isPasswordValid)
                {
                    _logger.LogInformation("User {LoginIdentifier} tried to login with invalid password.", userLoginDTO.LoginIdentifier);

                    return new GenericResultDTO<TokensDTO>(data: null, statusCode: StatusCodes.Status400BadRequest, errorsKeyValues: new Dictionary<string, HashSet<string>> { { "Credentials", new HashSet<string> { "invalid credentials" } } });
                }
                var jwtToken = await _jwtTokenService.GenerateTokens(userLoginDTO.LoginIdentifier,userLoginDTO.IsPersistence);
                _logger.LogInformation("User {LoginIdentifier} logged in successfully.", userLoginDTO.LoginIdentifier);
                return new GenericResultDTO<TokensDTO>(data:  jwtToken , statusCode: StatusCodes.Status201Created);

            }
        private Dictionary<string,HashSet<string>> GetIdentityErrors(IEnumerable<IdentityError> identityErrors)
        {
            var errors = new Dictionary<string, HashSet<string>>();
            foreach (IdentityError error in identityErrors)
            {
                if (!errors.ContainsKey(error.Code))
                {
                    errors[error.Code] = new HashSet<string>();
                }
                errors[error.Code].Add(error.Description);
            }
            return errors;
        }
           

        }
    }
