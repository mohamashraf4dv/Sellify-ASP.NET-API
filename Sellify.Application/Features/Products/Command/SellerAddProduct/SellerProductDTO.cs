
using Microsoft.AspNetCore.Http;

namespace Sellify.Application.Features.Products.Command.SellerAddProduct
{
    public record SellerProductDTO
        (
        string Name,
        string Description,
        decimal Price,
        IFormFile image
        );

}
