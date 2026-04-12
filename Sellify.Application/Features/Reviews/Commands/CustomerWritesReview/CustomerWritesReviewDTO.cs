namespace Sellify.Application.Features.Reviews.Commands.CustomerWritesReview
{
    public record CustomerWritesReviewDTO(string Description, int Score,Guid ProductId,bool IsReviewedAnonymously);

}
