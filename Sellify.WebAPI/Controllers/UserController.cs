using Sellify.Application.Features.Orders.Query.GetOrdersForAuthenticatedUser;
using Sellify.Application.Global.Results;

namespace Sellify.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class UserController : ControllerBase
    {
        private readonly IMediator _mediator;

        public UserController(IMediator mediator)
        {
            this._mediator = mediator;
        }
        [HttpGet]
        public async Task<ActionResult<GenericResultDTO<GetUserProfileQueryDTO>>> GetUserProfile()
        {
            var userId = User.FindFirstValue("sid");
           return await _mediator.Send(new GetUserProfileQuery(userId));
        }
        [HttpPut]
        public async Task<ActionResult<GenericResultDTO>> UpdateUserProfile(UpdateUserProfileDTO updateUserProfileDTO)
        {
            return await _mediator.Send(new UpdateUserProfileCommand(updateUserProfileDTO));
        }
        [HttpGet("Orders")]
        public async Task<ActionResult<GenericResultDTO>> GetUserOrders()
        {
            var userId = User.FindFirstValue("sid");
            return await _mediator.Send(new GetOrdersForAuthenticatedUserQuery(userId));
        }
    }
}
