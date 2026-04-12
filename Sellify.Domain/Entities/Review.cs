namespace Sellify.Domain.Entities
{
    public class Review
    {
        public Guid Id { get; set; }= Guid.NewGuid();
        public int Score { get; set; }
        public string? Description { get; set; }

        public Product Product { get; set; }
        public Guid ProductId { get; set; }

        public string ApplicationUserId { get; set; }
        public string ApplicationUserName { get; set; } = "Anonymous";

        public DateTime CreatedAt { get; set; }= DateTime.UtcNow;
    }
}
