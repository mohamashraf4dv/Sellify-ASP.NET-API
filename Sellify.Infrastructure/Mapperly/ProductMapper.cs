

using Riok.Mapperly.Abstractions;
using Sellify.Application.Features.Products.Command.SellerAddProduct;

namespace Sellify.Infrastructure.Mapperly
{
    [Mapper]
    public static partial class ProductMapper
    {
        [MapProperty(nameof(SellerProductDTO.Price), nameof(Product.Price))]
        [MapProperty(nameof(SellerProductDTO.Description), nameof(Product.Description))]
        [MapProperty(nameof(SellerProductDTO.ImageURL), nameof(Product.Thumbnail.Url))]
        [MapProperty(nameof(SellerProductDTO.Name), nameof(Product.Name))]
        public static partial Product SellerProductDtoToProduct(SellerProductDTO productDTO);

        private static decimal MapPrice(decimal? source) => source ?? 0;
        private static string MapStrings(string? source) => source ?? "";
        private static ProductImage MapThumbnail(ProductImage? source) => source ?? new ProductImage();
    }
}
