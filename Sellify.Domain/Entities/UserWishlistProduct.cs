namespace Sellify.Domain.Entities
{
    public class UserWishlistProduct
    {
        public Guid Id { get; set; }= Guid.NewGuid();
        public DateTime WishedAt { get; set; } = DateTime.UtcNow;

        //Navigation Property
        public Product Product { get; set; }

        //FKs
        public Guid ProductId { get; set; }
        public string UserId { get; set; }
    }
}
