namespace Sellify.Domain.Entities
{
    public class Review
    {
        public Guid Id { get; set; }= Guid.NewGuid();
        public int Score { get; set; }
        public string? Description { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public string ApplicationUserName { get; set; } = "Anonymous";

        public string ApplicationUserId { get; set; }
        public Guid ProductId { get; set; }
        //Navigation Property
        public Product Product { get; set; }

    }
}
