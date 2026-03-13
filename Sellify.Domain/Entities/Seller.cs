
namespace Sellify.Domain.Entities
{
    public class Seller
    {
        public string Id { get; set; }
        public ICollection<Product> Products { get; set; } = new HashSet<Product>();
        public bool IsActive { get; set; } = false;
        public decimal TotalEarned { get; set; } = 0;

    }
}
