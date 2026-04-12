namespace Sellify.Domain.Entities
{
    public class Review
    {
        public Guid Id { get; set; }
        public sbyte Score { get; set; }
        public string? Description { get; set; }

        public Product Product { get; set; }
        public Guid ProductId { get; set; }

        public string ApplicationUserId { get; set; }
        public string ApplicationUserName { get; set; }
    }
}
