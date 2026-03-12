using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Sellify.Application.Features.Products.Query.GetAllProducts;
using Sellify.Application.Global;

namespace Sellify.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IMediator _mediator;

        public ProductController(IMediator mediator)
        {
            this._mediator = mediator;
        }
        [HttpGet]
        public async Task<ActionResult<GenericResultDTO>> GetAll([FromQuery] int page=1 , [FromQuery] int number=11)
        {
            return await _mediator.Send(new GetAllProductsQuery(page,number));
        }
    }
}
