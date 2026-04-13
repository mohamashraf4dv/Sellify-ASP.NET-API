
namespace Sellify.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WishlistController : ControllerBase
    {
        private readonly IMediator _mediator;

        public WishlistController(IMediator mediator)
        {
            this._mediator = mediator;
        }
        [Authorize]
        [HttpPost()]
        public async Task<ActionResult<GenericResultDTO<WishlistStatus>>> AddWishlistProduct(NewWishlistDTO wishlistDTO)
        {
            var userId = User.FindFirstValue("sid");
            return await _mediator.Send(new NewWishlistCommand(wishlistDTO.ProductId, userId));
        }
        [Authorize]
        [HttpPut()]
        public async Task<ActionResult<GenericResultDTO<WishlistStatus>>> AddsWishlistProductIfNotExistAndRemoveItIfExists(NewWishlistDTO wishlistDTO)
        {
            var userId = User.FindFirstValue("sid");
            return await _mediator.Send(new UpdateWishlistStatusCommand(wishlistDTO.ProductId, userId));
        }
        [Authorize]
        [HttpGet()]
        public async Task<ActionResult<GenericResultDTO<IReadOnlyList<GetUserWishlistedProductsDTO>>>> GetWishlistsByUserId()
        {
            var userId = User.FindFirstValue("sid");
            return await _mediator.Send(new GetUserWishlistedProductsQuery(userId));
        }
    }
}
