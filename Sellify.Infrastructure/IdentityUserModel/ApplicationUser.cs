using Sellify.Domain.Enums;

namespace Sellify.Infrastructure.IdentityUserModel
{
    public class ApplicationUser:IdentityUser
    {
        public DateOnly DateOfBirth { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string FullName => $"{FirstName} {LastName}";
        public int Age => DateTime.Now.Year - DateOfBirth.Year;
        public SellerRoleRequestStatus? SellerRoleRequestStatus { get; set; } 
        public string? ImageURL { get; set; }
        //---- Navigation User
        public Seller? Seller { get; set; }

        // Each Application user Writes Many Reviews
        public ICollection<Review> Reviews { get; set; } = new HashSet<Review>();

        // Each Application User have Exactly one Token that gets updates eventually
        public Token Token { get; set; }

        // Each Application User can have Many Orders as a Buyer
        public ICollection<Order> Orders { get; set; } = new HashSet<Order>();

        //Each ApplicationUser have many WishListProducts
        public ICollection<UserWishlistProduct> UserWishlistProducts { get; set; } = new HashSet<UserWishlistProduct>();
    }
}
