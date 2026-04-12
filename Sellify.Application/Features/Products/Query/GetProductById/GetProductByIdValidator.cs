namespace Sellify.Application.Features.Products.Query.GetProductById
{
    public class GetProductByIdValidator:AbstractValidator<Product>
    {
        public GetProductByIdValidator()
        {
            RuleFor(x=> x.Id).NotEmpty().WithMessage("Product Id is Required");
        }
    }
}
