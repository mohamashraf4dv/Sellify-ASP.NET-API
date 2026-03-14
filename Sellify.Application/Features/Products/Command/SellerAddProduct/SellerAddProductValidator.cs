
namespace Sellify.Application.Features.Products.Command.SellerAddProduct
{
    public class SellerAddProductValidator:AbstractValidator<SellerAddProductCommand>
    {
        public SellerAddProductValidator()
        {
            RuleFor(x => x.product.Price).GreaterThan(0).WithMessage("Price shouldn't be less than 0")
                .NotEmpty().WithMessage("Price is required");

            RuleFor(x => x.product.Name).NotEmpty().WithMessage("Name is required");

            RuleFor(x => x.product.Thumbnail!.Url).NotEmpty().WithMessage("Image must be uploaded");

            RuleFor(x => x.product.SellerId).NotEmpty().WithMessage("Seller you cannot add product without a SellerId");
        }
    }
}
