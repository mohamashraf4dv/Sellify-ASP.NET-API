using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Sellify.Application.Features.Products.Command.UserWishlistsProduct.Command.NewWishlist;
using Sellify.Application.Features.Products.Command.UserWishlistsProduct.Command.UpdateWishlistStatus;
using Sellify.Application.Global;
using Sellify.Domain.Enums;
using System.Security.Claims;

namespace Sellify.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WishlistController : ControllerBase
    {
        private readonly IMediator _mediator;

        public WishlistController(IMediator mediator)
        {
            this._mediator = mediator;
        }
        [Authorize]
        [HttpPost()]
        public async Task<ActionResult<GenericResultDTO<WishlistStatus>>> AddWishlistProduct(NewWishlistDTO wishlistDTO)
        {
            var userId = User.FindFirstValue("sid");
            return await _mediator.Send(new NewWishlistCommand(wishlistDTO.ProductId, userId));
        }
        [Authorize]
        [HttpPut()]
        public async Task<ActionResult<GenericResultDTO<WishlistStatus>>> AddsWishlistProductIfNotExistAndRemoveItIfExists(NewWishlistDTO wishlistDTO)
        {
            var userId = User.FindFirstValue("sid");
            return await _mediator.Send(new UpdateWishlistStatusCommand(wishlistDTO.ProductId, userId));
        }
    }
}
