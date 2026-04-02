
namespace Sellify.Application.Features.Products.Command.SellerAddProduct
{
    public class SellerAddProductValidator:AbstractValidator<SellerAddProductCommand>
    {
        public SellerAddProductValidator()
        {
            RuleFor(x => x.ProductDTO).NotEmpty().WithMessage("Invalid Data")
                .ChildRules(product =>
                {
                    product.RuleFor(x => x.Name).NotEmpty().WithMessage("Name is required");
                    product.RuleFor(x => x.Price).GreaterThan(0).WithMessage("Price shouldn't be less than 0")
                        .NotEmpty().WithMessage("Price is required");
                });
            //RuleFor(x => x.ProductDTO.Price).GreaterThan(0).WithMessage("Price shouldn't be less than 0")
            //    .NotEmpty().WithMessage("Price is required");

            //RuleFor(x => x.ProductDTO.Name).NotEmpty().WithMessage("Name is required");

            //RuleFor(x => x.product.Thumbnail!.Url).NotEmpty().WithMessage("Image must be uploaded");

        }
    }
}
