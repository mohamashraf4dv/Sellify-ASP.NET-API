namespace Sellify.Application.Features.Products.Command.SellerAddProduct
{
    public record SellerAddProductCommand(SellerProductDTO product) : IRequest<GenericResultDTO>;

}
