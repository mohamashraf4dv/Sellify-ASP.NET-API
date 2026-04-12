namespace Sellify.Application.Features.Reviews.Commands.CustomerWritesReview
{
    public class CustomerWritesReviewValidator:AbstractValidator<CustomerWritesReviewCommand>
    {
        public CustomerWritesReviewValidator()
        {
            RuleFor(x=> x.ProductId).NotEmpty().WithMessage("ProductId is Required");
            RuleFor(x=> x.Description).NotEmpty().WithMessage("Description is Required");
            RuleFor(x=> x.Score).GreaterThan(0).WithMessage("Score should be more than 0");
            RuleFor(x=> x.Score).LessThan(6).WithMessage("Score should be less than 6");
            RuleFor(x=> x.UserId).NotEmpty().WithMessage("UserId is Required you are unauthorized");
        }
    }
}
