
namespace Sellify.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class WishlistController : ControllerBase
    {
        private readonly IMediator _mediator;

        public WishlistController(IMediator mediator)
        {
            this._mediator = mediator;
        }
        [HttpPost()]
        public async Task<ActionResult<GenericResultDTO<WishlistStatus>>> AddWishlistProduct(NewWishlistDTO wishlistDTO)
        {
            var userId = User.FindFirstValue("sid");
            return await _mediator.Send(new NewWishlistCommand(wishlistDTO.ProductId, userId));
        }
        [HttpPut()]
        public async Task<ActionResult<GenericResultDTO<WishlistStatus>>> AddsWishlistProductIfNotExistAndRemoveItIfExists(NewWishlistDTO wishlistDTO)
        {
            var userId = User.FindFirstValue("sid");
            return await _mediator.Send(new UpdateWishlistStatusCommand(wishlistDTO.ProductId, userId));
        }
        [HttpGet()]
        public async Task<ActionResult<GenericResultDTO<IReadOnlyList<GetUserWishlistedProductsDTO>>>> GetWishlistsByUserId()
        {
            var userId = User.FindFirstValue("sid");
            return await _mediator.Send(new GetUserWishlistedProductsQuery(userId));
        }
    }
}
