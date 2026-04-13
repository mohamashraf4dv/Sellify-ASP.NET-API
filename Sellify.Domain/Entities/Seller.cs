
namespace Sellify.Domain.Entities
{
    public class Seller
    {
        public string Id { get; set; }
        public bool IsActive { get; set; } = true;
        //Navigation Property
        public ICollection<Product> Products { get; set; } = new HashSet<Product>();


    }
}
