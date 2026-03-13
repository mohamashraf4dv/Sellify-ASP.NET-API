
namespace Sellify.Application.Features.Products.Command.SellerAddProduct
{
    public record SellerProductDTO
        (
        string name,
        string description,
        string price,
        string imageURL
        );

}
