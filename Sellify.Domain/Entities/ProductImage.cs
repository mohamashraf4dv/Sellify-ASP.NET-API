
namespace Sellify.Domain.Entities
{
    public class ProductImage
    {
        public Guid Id { get; set; }
        public string Description { get; set; }
        public string Url { get; set; }

        //Navigation Property
        public Product Product { get; set; }
    }
}
