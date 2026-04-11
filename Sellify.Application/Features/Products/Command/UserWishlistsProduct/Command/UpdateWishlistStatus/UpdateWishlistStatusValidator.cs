namespace Sellify.Application.Features.Products.Command.UserWishlistsProduct.Command.UpdateWishlistStatus
{
    public class UpdateWishlistStatusValidator:AbstractValidator<UserWishlistProduct>
    {
        public UpdateWishlistStatusValidator()
        {
            RuleFor(x => x.UserId).NotEmpty().WithMessage("Unauthorized user cannot wishlist");
            RuleFor(x => x.ProductId).NotEmpty().WithMessage("ProductId cannot be empty");
        }
    }
}
