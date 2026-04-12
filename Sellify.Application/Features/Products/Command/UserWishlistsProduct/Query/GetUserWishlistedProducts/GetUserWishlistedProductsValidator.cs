namespace Sellify.Application.Features.Products.Command.UserWishlistsProduct.Query.GetUserWishlistedProducts
{
    public class GetUserWishlistedProductsValidator:AbstractValidator<GetUserWishlistedProductsQuery>
    {
        public GetUserWishlistedProductsValidator()
        {
            RuleFor(x => x.UserId).NotEmpty().WithMessage("UnAuthorized User");
        }
    }
}
