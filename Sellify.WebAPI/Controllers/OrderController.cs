using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Sellify.Application.Features.Order.Command.CreateOrder;

namespace Sellify.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        [HttpPost]
        public async Task<ActionResult> CreateOrder(IReadOnlyList<CreateOrderDTO> orderDTOs)
        {
            return Ok(orderDTOs);
        }
    }
}
