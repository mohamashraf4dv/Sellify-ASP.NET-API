

using Riok.Mapperly.Abstractions;
using Sellify.Application.Features.Products.Command.SellerAddProduct;

namespace Sellify.Infrastructure.Mapperly
{
    [Mapper]
    public static partial class ProductMapper
    {
        [MapProperty(nameof(SellerProductDTO.price),nameof(Product.Price))]
        [MapProperty(nameof(SellerProductDTO.description),nameof(Product.Description))]
        [MapProperty(nameof(SellerProductDTO.imageURL),nameof(Product.Thumbnail.Url))]
        [MapProperty(nameof(SellerProductDTO.name),nameof(Product.Name))]
        public static partial Product SellerProductDtoToProduct(SellerProductDTO productDTO);
    }
}
