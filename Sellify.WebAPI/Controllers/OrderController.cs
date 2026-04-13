namespace Sellify.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class OrderController : ControllerBase
    {
        private readonly IMediator _mediator;

        public OrderController(IMediator mediator) 
        {
            this._mediator = mediator;
        }
        [HttpPost]
        public async Task<ActionResult<GenericResultDTO>> CreatePaymentSession(CreatePaymentSessionRequest createPaymentSessionRequest)
        {
            var userId = User.FindFirstValue("sid");
            return await _mediator.Send(new CreatePaymentSessionCommand(createPaymentSessionRequest.Order, createPaymentSessionRequest.ProductsIds,userId));
        }
        [HttpPost("Success")]
        public async Task<ActionResult<GenericResultDTO>> CreateOrder([FromBody] CreateOrderDTO createOrderDTO)
        {
            return await _mediator.Send(new CreateOrderCommand(createOrderDTO.SessionId));
        }
    }
}
