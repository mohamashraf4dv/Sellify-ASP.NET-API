namespace Sellify.Application.Features.Products.Command.UserWishlistsProduct.Query.GetUserWishlistedProducts
{
    public record GetUserWishlistedProductsDTO
        (
        Guid ProductId,
        string ThumbnailSource,
        string ProductName,
        decimal ProductPrice,
        long Stock,
        DateTime WishedAtDate
        );
}
