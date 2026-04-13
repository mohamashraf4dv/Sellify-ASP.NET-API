namespace Sellify.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReviewController : ControllerBase
    {
        private readonly IMediator _mediator;

        public ReviewController(IMediator mediator)
        {
            this._mediator = mediator;
        }
        [HttpPost]
        [Authorize]
        public async Task<GenericResultDTO> CreateNewReview(CustomerWritesReviewDTO reviewDTO)
        {
            var userId = User.FindFirstValue("sid");
            var userFullname = reviewDTO.IsReviewedAnonymously ? "Anonymous" : User.FindFirstValue("name");
           var result =await _mediator.Send(new CustomerWritesReviewCommand(userId,userFullname,reviewDTO.Description,reviewDTO.Score,reviewDTO.ProductId));
            return result;
        }
    }
}
