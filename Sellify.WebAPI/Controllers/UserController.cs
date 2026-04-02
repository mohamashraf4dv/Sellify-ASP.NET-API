using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Sellify.Application.Features.Authentication.Commands.UpdateUserProfile;
using Sellify.Application.Features.Authentication.Query.GetUserProfile;
using Sellify.Application.Global;

namespace Sellify.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IMediator _mediator;

        public UserController(IMediator mediator)
        {
            this._mediator = mediator;
        }
        [Authorize]
        [HttpGet]
        public async Task<ActionResult<GenericResultDTO<GetUserProfileQueryDTO>>> GetUserProfile()
        {
            var refreshToken = Request.Cookies?["bearer"];
           return await _mediator.Send(new GetUserProfileQuery(refreshToken));
        }
        [Authorize]
        [HttpPut]
        public async Task<ActionResult<GenericResultDTO>> UpdateUserProfile(UpdateUserProfileDTO updateUserProfileDTO)
        {
            return await _mediator.Send(new UpdateUserProfileCommand(updateUserProfileDTO));
        }
    }
}
