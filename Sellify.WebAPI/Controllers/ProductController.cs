using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Sellify.Application.Contracts.Repositories;
using Sellify.Application.Contracts.Services;
using Sellify.Application.Features.Products.Command.SellerAddProduct;
using Sellify.Application.Features.Products.Query.GetAllProducts;
using Sellify.Application.Features.Products.Query.GetProductById;
using Sellify.Application.Global;
using Sellify.Domain.Entities;
using Sellify.Infrastructure.Mapperly;
using System.Security.Claims;

namespace Sellify.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IWebHostEnvironment environment;

        public ProductController(IMediator mediator,IWebHostEnvironment environment,IProductRepository productRepository)
        {
            this._mediator = mediator;
            this.environment = environment;
        }
        [HttpGet]
        public async Task<ActionResult<GenericResultDTO<GetAllProductsWithNextOptionDTO>>> GetAll([FromQuery] int page = 1, [FromQuery] int number = 11)
        {
            return await _mediator.Send(new GetAllProductsQuery(page, number));
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<GenericResultDTO<GetProductByIdDTO>>> GetById([FromRoute] Guid id)
        {
           return await _mediator.Send(new GetProductByIdQuery(id));
        }
        [Authorize(Roles = "Seller")]
        [HttpPost]
        public async Task<ActionResult<GenericResultDTO>> CreateNew([FromForm] SellerProductDTO productDto)
        {
            var userId = User.FindFirstValue("sid");
            return await _mediator.Send(new SellerAddProductCommand(productDto, userId));
        }

    }
}
