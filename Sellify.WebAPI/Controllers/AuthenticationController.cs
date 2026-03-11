using FluentValidation;
using FluentValidation.Results;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Sellify.Application.Features.Authentication.Commands.InternalUserLogin;
using Sellify.Application.Features.Authentication.Commands.UserRegisteration;
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
        public async Task<ActionResult> Register([FromBody] UserRegisterationDTO userRegisterationDTO)
        {
            var result = await _mediator.Send(new UserRegisterationCommand() { userRegisteration = userRegisterationDTO });
            return StatusCode(result.statusCode,result);
        }
        [HttpPost("login")]
        public async Task<ActionResult> InternalLogin([FromBody] InternalUserLoginDTO userLoginDTO)
        {
            var result = await _mediator.Send(new InternalUserLoginCommand() { userLoginDTO = userLoginDTO });
            return StatusCode(result.statusCode, result);
        }

    }
}