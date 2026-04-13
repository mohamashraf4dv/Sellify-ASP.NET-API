namespace Sellify.WebAPI.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class TokenController : ControllerBase
    {
        private readonly IMediator _mediator;

        public TokenController(IMediator mediator)
        {
            _mediator = mediator;
        }
        [HttpGet("User")]
        public ActionResult GetApplicationUser() 
        {
            var name = User.Identity.Name;
            return Ok(name);
        }
        [AllowAnonymous]
        [HttpPost("refresh")]
        public async Task<ActionResult> RefreshToken() 
        {
            string refreshToken = Request.Cookies?["bearer"];
            if (refreshToken is null)
                return BadRequest();
            var result = await _mediator.Send(new UpdateAccessTokenCommand(refreshToken));
            return StatusCode(result.statusCode,new {token= result.data?.AccessToken?? ""});
        }
        [HttpPost("revoke")]
        public async Task<ActionResult> RevokeToken()
        {
            string refreshToken = Request.Cookies?["bearer"];
            var result = await _mediator.Send(new RevokeTokenCommand(refreshToken));
            Response.Cookies.Delete("bearer", new CookieOptions {
                HttpOnly = true ,
                Secure = true,
                SameSite = SameSiteMode.None,
                });
            return StatusCode(result.StatusCode);
        }
    }
}
