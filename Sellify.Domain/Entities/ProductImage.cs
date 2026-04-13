
namespace Sellify.Domain.Entities
{
    public class ProductImage
    {
        public Guid Id { get; set; }
        public string? Description { get; set; }
        public string Url { get; set; }
        public bool IsThumbnail { get; set; } = false;
        //Navigation Property
        public Product Product { get; set; }
        public Guid ProductId { get; set; }

    }
}
