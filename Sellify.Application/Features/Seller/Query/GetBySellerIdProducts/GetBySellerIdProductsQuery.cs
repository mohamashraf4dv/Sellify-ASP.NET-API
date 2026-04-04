namespace Sellify.Application.Features.Seller.Query.GetBySellerIdProducts
{
    public record GetBySellerIdProductsQuery(string SellerId) : IRequest<GenericResultDTO<IReadOnlyList<GetBySellerIdProductsQueryDTO>>>;

}
