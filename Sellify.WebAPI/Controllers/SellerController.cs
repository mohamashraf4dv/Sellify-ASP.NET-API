using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Sellify.Application.Features.Seller.Command.RequestRole;
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
        [HttpPost("Apply")]
        public async Task<ActionResult<GenericResultDTO>> SellerApplying()
        {
            var refreshToken = Request.Cookies?["bearer"];
            var result = await _mediator.Send(new RequestRoleCommand(refreshToken));
            return result;
            //-- get profile if ==> NULL OR Rejected OK Else Bad Request
        }
        [HttpPost("Approve")]
        public async Task<ActionResult<GenericResultDTO>> SellerApprove()
        {
            var refreshToken = Request.Cookies?["bearer"];
            var result = await _mediator.Send(new RequestRoleCommand(refreshToken,SellerRoleRequestStatus.Approved));
            return result;
            //-- get profile if ==> NULL OR Rejected OK Else Bad Request
        }
        [HttpPost("Reject")]
        public async Task<ActionResult<GenericResultDTO>> SellerReject()
        {
            var refreshToken = Request.Cookies?["bearer"];
            var result = await _mediator.Send(new RequestRoleCommand(refreshToken,SellerRoleRequestStatus.Rejected));
            return result;
            //-- get profile if ==> NULL OR Rejected OK Else Bad Request
        }
    }
}
