using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Sellify.Application.Contracts.Services;
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
        private readonly ITokenService _jwtTokenService;

        public ProductController(IMediator mediator,ITokenService jwtTokenService)
        {
            this._mediator = mediator;
            this._jwtTokenService = jwtTokenService;
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

        [Authorize]
        [HttpGet("test")]
        public async Task<ActionResult> Test()
        {

            return Ok("Your signedIn");
        }
        [HttpGet("Update")]
        public async Task<ActionResult> Update([FromQuery] string refreshToken)
        {

            var token = await _jwtTokenService.UpdateExistingAccessToken(refreshToken);
            return Ok(token);
        }

    }
}
