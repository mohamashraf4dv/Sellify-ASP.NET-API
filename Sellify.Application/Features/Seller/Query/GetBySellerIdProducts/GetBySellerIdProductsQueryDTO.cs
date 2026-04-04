namespace Sellify.Application.Features.Seller.Query.GetBySellerIdProducts
{
    public record GetBySellerIdProductsQueryDTO( 
     Guid Id ,
     string Name ,
     decimal Price ,
     long Stock ,
     ICollection<string> ProductImages,
     string ThumbnailSource
     );

}
