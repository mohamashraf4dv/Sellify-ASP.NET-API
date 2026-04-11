using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Sellify.Application.Features.Authentication.Commands.UpdateUserProfile;
using Sellify.Application.Features.Authentication.Query.GetUserProfile;
using Sellify.Application.Global;
using System.Security.Claims;

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
            var userId = User.FindFirstValue("sid");
           return await _mediator.Send(new GetUserProfileQuery(userId));
        }
        [Authorize]
        [HttpPut]
        public async Task<ActionResult<GenericResultDTO>> UpdateUserProfile(UpdateUserProfileDTO updateUserProfileDTO)
        {
            return await _mediator.Send(new UpdateUserProfileCommand(updateUserProfileDTO));
        }

    }
}
