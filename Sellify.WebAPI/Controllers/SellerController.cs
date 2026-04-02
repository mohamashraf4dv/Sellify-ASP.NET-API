using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Sellify.Application.Features.Seller.Command.RequestRole;
using Sellify.Application.Features.Seller.Query.GetBySellerIdProducts;
using Sellify.Application.Global;
using Sellify.Domain.Enums;

namespace Sellify.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SellerController : ControllerBase
    {
        private readonly IMediator _mediator;

        public SellerController(IMediator mediator)
        {
            this._mediator = mediator;
        }
        [Authorize]
        [HttpPost("Apply")]
        public async Task<ActionResult<GenericResultDTO>> SellerApplying()
        {
            var refreshToken = Request.Cookies?["bearer"];
            var result = await _mediator.Send(new RequestRoleCommand(refreshToken));
            return result;
            //-- get profile if : NULL Else => BAD REQUEST
        }
        [HttpGet("{id}/Products")]
        public async Task<ActionResult<GenericResultDTO<IReadOnlyList<GetBySellerIdProductsQueryDTO>>>> GetSellerProductsById(string id)
        {
            var result = await _mediator.Send(new GetBySellerIdProductsQuery(id));
            return result;
        }
        #region this might be added in the future
        /*
        [Authorize("Admin")]
        [HttpPost("Approve")]
        public async Task<ActionResult<GenericResultDTO>> SellerApprove()
        {
            var refreshToken = Request.Cookies?["bearer"];
            var result = await _mediator.Send(new RequestRoleCommand(refreshToken, SellerRoleRequestStatus.Approved));
            return result;
        }
        [Authorize("Admin")]
        [HttpPost("Reject")]
        public async Task<ActionResult<GenericResultDTO>> SellerReject()
        {
            var refreshToken = Request.Cookies?["bearer"];
            var result = await _mediator.Send(new RequestRoleCommand(refreshToken, SellerRoleRequestStatus.Rejected));
            return result;
        } 
        */
        #endregion
    }
}
