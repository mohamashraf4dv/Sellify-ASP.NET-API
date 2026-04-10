using FluentValidation;
using FluentValidation.Results;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Sellify.Application.Features.Authentication.Commands.InternalUserLogin;
using Sellify.Application.Features.Authentication.Commands.UserRegisteration;
using Sellify.Application.Features.Token;
using Sellify.Application.Global;
using Sellify.Infrastructure.IdentityUserModel;
using Sellify.Infrastructure.Mapperly;
namespace Sellify.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        private readonly IMediator _mediator;

        public AuthenticationController(IMediator mediator)
        {
            _mediator = mediator;
        }
        [HttpPost("register")]
        public async Task<ActionResult<GenericResultDTO<TokensDTO>>> Register([FromBody] UserRegisterationDTO userRegisterationDTO)
        {
            var result = await _mediator.Send(new UserRegisterationCommand() { userRegisteration = userRegisterationDTO });
            return StatusCode(result.statusCode,result);
        }

        [HttpPost("login")]
        public async Task<ActionResult> InternalLoginTest([FromBody] InternalUserLoginDTO userLoginDTO)
        {
            var refreshTokenCookieExpirationInDays = 30;
            var result = await _mediator.Send(new InternalUserLoginCommand() { userLoginDTO = userLoginDTO });

            if(result.data?.RefreshToken is not null)
                    Response.Cookies.Append("bearer",result.data.RefreshToken
                        ,new CookieOptions { 
                        HttpOnly=true ,
                        Secure=true,
                        SameSite=SameSiteMode.None,
                        Expires=DateTimeOffset.UtcNow.AddDays(refreshTokenCookieExpirationInDays)});
            return StatusCode(result.statusCode, new GenericResultDTO(result.data?.AccessToken,result.statusCode));
        }
    }
}