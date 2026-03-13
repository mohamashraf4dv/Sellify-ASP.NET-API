namespace Sellify.Infrastructure.IdentityUserModel
{
    public class ApplicationUser:IdentityUser
    {
        public DateOnly DateOfBirth { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string FullName => $"{FirstName} {LastName}";
        public int Age => DateTime.Now.Year - DateOfBirth.Year;

        public string ImageURL { get; set; }
        //---- Navigation User
        public Seller? Seller { get; set; }

        // Each Application user Writes Many Reviews
        public ICollection<Review> Reviews { get; set; } = new HashSet<Review>();
    }
}
