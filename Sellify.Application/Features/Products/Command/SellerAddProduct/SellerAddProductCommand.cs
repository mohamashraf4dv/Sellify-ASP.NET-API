namespace Sellify.Application.Features.Products.Command.SellerAddProduct
{
    public record SellerAddProductCommand(Product product) : IRequest<GenericResultDTO>;

}
