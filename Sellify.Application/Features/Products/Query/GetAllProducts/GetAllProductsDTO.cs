namespace Sellify.Application.Features.Products.Query.GetAllProducts
{
    public class GetAllProductsDTO
    {
        public Guid Id { get; set; }
        public string ProductName { get; set; }
        public decimal Price { get; set; }
        public decimal Stock { get; set; }
        public string ThumbnailSource { get; set; }
        public string SellerFullName { get; set; }
        public byte AverageScore { get; set; }
    }
}
