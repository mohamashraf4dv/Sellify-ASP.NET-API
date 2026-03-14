using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Sellify.Application.Features.Products.Command.SellerAddProduct;
using Sellify.Application.Features.Products.Query.GetAllProducts;
using Sellify.Application.Global;
using Sellify.Domain.Entities;
using Sellify.Infrastructure.Mapperly;

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
        public async Task<ActionResult<GenericResultDTO>> GetAll([FromQuery] int page = 1, [FromQuery] int number = 11)
        {
            return await _mediator.Send(new GetAllProductsQuery(page, number));
        }
        [HttpPost]
        public async Task<ActionResult<GenericResultDTO>> CreateNew(SellerProductDTO productDto)
        {
            Product product = ProductMapper.SellerProductDtoToProduct(productDto);
            return await _mediator.Send(new SellerAddProductCommand(product));
        }
        //[HttpPost("TestProduct")]
        //public async Task<ActionResult<Product>> CreateNewTest(SellerProductDTO product)
        //{
        //    Product product1 = ProductMapper.SellerProductDtoToProduct(product);
        //    return product1;
        //}
    }
}
