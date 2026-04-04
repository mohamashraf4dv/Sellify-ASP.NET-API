namespace Sellify.Application.Features.Products.Command.SellerUpdateProducts
{
    public record SellerUpdateProductsCommand(IReadOnlyList<SellerUpdateProductsDTO> Products) : IRequest<GenericResultDTO>;

}
