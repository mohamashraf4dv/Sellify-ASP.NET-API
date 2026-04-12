namespace Sellify.Application.Features.Products.Query.GetProductById
{
    public record GetProductByIdDTO
    (Guid Id, string Thumbnail,decimal Price,string Description, long Stock,string Name,bool IsWishlisted,
        ICollection<GetProductByIdReviews> GetProductByIdReviewsDTO);
//    public record GetProductByIdDTO
//(Guid ProductId, string ProductImage, decimal ProductPrice, string ProductDescription, long ProductsInStock,
//    ICollection<GetProductByIdReviews> GetProductByIdReviewsDTO, string SellerName, string SellerImage);

    public record GetProductByIdReviews(string Review, string User, int Stars);
}
