namespace Sellify.Application.Features.Reviews.Commands.CustomerWritesReview
{
    public record CustomerWritesReviewCommand(string UserId,string UserFullName, string Description, int Score,Guid ProductId) : IRequest<GenericResultDTO>;

}
