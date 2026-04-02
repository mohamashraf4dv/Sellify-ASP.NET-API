using Microsoft.AspNetCore.Http;

namespace Sellify.Application.Features.Products.Command.SellerAddProduct
{
    public record SellerAddProductCommand(SellerProductDTO ProductDTO,string SellerId) : IRequest<GenericResultDTO>;

}
