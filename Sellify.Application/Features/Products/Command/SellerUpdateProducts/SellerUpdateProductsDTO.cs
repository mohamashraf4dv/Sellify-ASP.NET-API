using Microsoft.AspNetCore.Http;

namespace Sellify.Application.Features.Products.Command.SellerUpdateProducts
{
    public record SellerUpdateProductsDTO(
         Guid Id,
         string Name,
         decimal Price,
         long Stock
        //IReadOnlyList<IFormFile>? ProductImages //might remove this when we deploy the application --> then we update products without images feature (will appear locally only)
    );

}
