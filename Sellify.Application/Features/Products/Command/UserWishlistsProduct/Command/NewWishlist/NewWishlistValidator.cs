namespace Sellify.Application.Features.Products.Command.UserWishlistsProduct.Command.NewWishlist
{
    public class NewWishlistValidator:AbstractValidator<NewWishlistCommand>
    {
        public NewWishlistValidator()
        {
            RuleFor(x => x.UserId).NotEmpty().WithMessage("Unauthorized user cannot wishlist");
            RuleFor(x => x.ProductId).NotEmpty().WithMessage("ProductId cannot be empty");
        }
    }
}
