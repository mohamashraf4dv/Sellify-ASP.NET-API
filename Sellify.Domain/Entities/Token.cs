namespace Sellify.Domain.Entities
{
    public class Token
    {
        public  string? AccessToken { get; set; }
        public  string? RefreshToken { get; set; }
        public DateTime IssuedDate { get; set; }= DateTime.UtcNow;
        public DateTime ExpirationDate { get; set; } = DateTime.UtcNow.AddDays(7);
        public bool IsRevoked { get; set; } = false;
        public DateTime? RevokationDate { get; set; } = null;
        public bool IsExpired => ExpirationDate <= IssuedDate;

        //FK
        public required string ApplicationUserId { get; set; }
    }
}
