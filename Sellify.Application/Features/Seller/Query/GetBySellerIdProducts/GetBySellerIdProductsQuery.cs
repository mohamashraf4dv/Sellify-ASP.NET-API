namespace Sellify.Application.Features.Seller.Query.GetBySellerIdProducts
{
    public record GetBySellerIdProductsQuery(string sellerId) : IRequest<GenericResultDTO<IReadOnlyList<GetBySellerIdProductsQueryDTO>>>;

}
