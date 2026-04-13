

namespace Sellify.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PaymentController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IPaymentService _paymentService;

        public PaymentController(IMediator mediator , IPaymentService paymentService)
        {
            this._mediator = mediator;
            this._paymentService = paymentService;
        }

        [HttpGet("StripeSecret")]
        public async Task<ActionResult<GenericResultDTO>> GetStripeClientSecret([FromQuery] decimal totalAmount)
        {
            return await _mediator.Send(new GetPaymentIntentClientSecretQuery( totalAmount ));

        }
        [Authorize(Roles = "Admin")]
        [HttpGet("Session")]
        public async Task<ActionResult<GenericResultDTO>> AdminPaymentSessionInvestegation([FromQuery] string sessionId)
        {
            ///----- This Violates Clean Architecture & will be modified ! -----
            SessionService session = new SessionService();
            var sessionGot = await session.GetAsync(sessionId);
            return Ok(sessionGot);
        }


        #region Hook For Scaling in the future
        //-- Might use it in the future after i deploy
        //[HttpPost("webhook")]
        //public async Task<IActionResult> StripeWebhook()
        //{
        //    var json = await new StreamReader(HttpContext.Request.Body).ReadToEndAsync();

        //    try
        //    {
        //        var stripeEvent = EventUtility.ConstructEvent(
        //            json,
        //            Request.Headers["Stripe-Signature"],
        //            "your_webhook_secret"
        //        );
        //        string Id = null;

        //        if (stripeEvent.Type == "checkout.session.completed")
        //        {
        //            var session = stripeEvent.Data.Object as Session;

        //            //--- Mark order as paid -> modify quantity ---
        //            var sessionId = session?.Id;
        //            Id = sessionId;
        //        }

        //        return Ok(Id);
        //    }
        //    catch (Exception)
        //    {
        //        return BadRequest();
        //    }
        //} 
        #endregion
    }
}
