namespace Sellify.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SellerController : ControllerBase
    {
        private readonly IMediator _mediator;

        public SellerController(IMediator mediator)
        {
            this._mediator = mediator;
        }
        [Authorize]
        [HttpPost("Apply")]
        public async Task<ActionResult<GenericResultDTO>> SellerApplying()
        {
            var refreshToken = Request.Cookies?["bearer"];
            return await _mediator.Send(new RequestRoleCommand(refreshToken));
            //-- get profile if : NULL Else => BAD REQUEST
        }
        [HttpGet("Products")]
        [Authorize]
        public async Task<ActionResult<GenericResultDTO<IReadOnlyList<GetBySellerIdProductsQueryDTO>>>> GetSellerProductsById([FromQuery]string? sellerId)
        {
            sellerId ??= User.Claims.FirstOrDefault(c => c.Type == "sid")?.Value;
            return await _mediator.Send(new GetBySellerIdProductsQuery(sellerId));
             
        }
        [HttpPut("Products")]
        [Authorize(Roles ="Seller")]
        public async Task<ActionResult<GenericResultDTO>> UpdateChangedProducts(IReadOnlyList<SellerUpdateProductsDTO> products)
        {
            return await _mediator.Send(new SellerUpdateProductsCommand(products));
        }
        #region this might be added in the future
        /*
        [Authorize("Admin")]
        [HttpPost("Approve")]
        public async Task<ActionResult<GenericResultDTO>> SellerApprove()
        {
            var refreshToken = Request.Cookies?["bearer"];
            var result = await _mediator.Send(new RequestRoleCommand(refreshToken, SellerRoleRequestStatus.Approved));
            return result;
        }
        [Authorize("Admin")]
        [HttpPost("Reject")]
        public async Task<ActionResult<GenericResultDTO>> SellerReject()
        {
            var refreshToken = Request.Cookies?["bearer"];
            var result = await _mediator.Send(new RequestRoleCommand(refreshToken, SellerRoleRequestStatus.Rejected));
            return result;
        } 
        */
        #endregion
    }
}
