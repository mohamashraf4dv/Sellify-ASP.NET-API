using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Sellify.Application.Contracts.Services;
using Sellify.Application.Features.Payment.Query.GetStripePaymentIntentClientSecret;
using Sellify.Application.Global;
using Sellify.Infrastructure.Services;
using Stripe;
using Stripe.Checkout;

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
        [HttpGet("TEST")]
        public async Task<ActionResult<GenericResultDTO>> TEST([FromQuery] string sessionId)
        {
            SessionService session = new SessionService();
            var lineItemService = new SessionLineItemService();
            var list =lineItemService.List(sessionId);
           var sessionGot= await session.GetAsync(sessionId);
            return Ok(list);
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
