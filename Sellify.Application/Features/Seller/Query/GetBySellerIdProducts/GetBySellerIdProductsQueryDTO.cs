namespace Sellify.Application.Features.Seller.Query.GetBySellerIdProducts
{
    public record GetBySellerIdProductsQueryDTO( 
     Guid Id ,
     string ProductName ,
     decimal Price ,
     decimal Stock ,
     string ImageURL 
     );

}
