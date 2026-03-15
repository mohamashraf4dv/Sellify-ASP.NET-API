using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Sellify.Application.Features.Token.Query.GetToken;

namespace Sellify.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TokenController : ControllerBase
    {
        private readonly IMediator _mediator;

        public TokenController(IMediator mediator)
        {
            _mediator = mediator;
        }
        [Authorize]
        [HttpGet("User")]
        public ActionResult GetApplicationUser() 
        {
            return Ok();
        }

        [HttpGet]
        public async Task<ActionResult> GetTokens() 
        {
            string refreshToken = Request.Cookies?["bearer"];
            var result = await _mediator.Send(new GetTokensQuery(refreshToken,"ssas"));
            return Ok();
        }
    }
}
