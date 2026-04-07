using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Sellify.Application.Features.Orders.Command.CreateOrder;
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
        public async Task<ActionResult<GenericResultDTO>> CreateOrder(CreateOrderRequest createOrderRequest)
        {
            var userId = User.FindFirstValue("sid");
            return await _mediator.Send(new CreateOrderCommand(createOrderRequest.Order,createOrderRequest.ProductsIds,userId));
        }
    }
}
