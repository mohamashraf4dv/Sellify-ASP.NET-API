
namespace Sellify.Application.Features.Seller.Query.GetBySellerIdProducts
{
    public class GetBySellerIdProductsQueryValidator:AbstractValidator<GetBySellerIdProductsQuery>
    {
        public GetBySellerIdProductsQueryValidator()
        {
            
            RuleFor(x => x.SellerId).NotEmpty().WithMessage("Cannot Find Products When SellerId is Empty");
        }
    }
}
