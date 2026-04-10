using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Sellify.Application.Features.Orders.Command.CreateOrder;
using Sellify.Application.Features.Payment.Command.CreatePaymentIntent;
using Sellify.Application.Features.Payment.Command.CreatePaymentSession;
using Sellify.Application.Global;
using System.Security.Claims;

namespace Sellify.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly IMediator _mediator;

        public OrderController(IMediator mediator) 
        {
            this._mediator = mediator;
        }
        [HttpPost]
        [Authorize]
        public async Task<ActionResult<GenericResultDTO>> CreatePaymentSession(CreatePaymentSessionRequest createPaymentSessionRequest)
        {
            var userId = User.FindFirstValue("sid");
            return await _mediator.Send(new CreatePaymentSessionCommand(createPaymentSessionRequest.Order, createPaymentSessionRequest.ProductsIds,userId));
        }
        [HttpPost("Success")]
        [Authorize]
        public async Task<ActionResult<GenericResultDTO>> CreateOrder([FromBody] CreateOrderDTO createOrderDTO)
        {
            return await _mediator.Send(new CreateOrderCommand(createOrderDTO.SessionId));
        }
    }
}
