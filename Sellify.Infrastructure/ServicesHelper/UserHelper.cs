
namespace Sellify.Infrastructure.ServicesHelper
{
    public class UserHelper
    {
        private UserManager<ApplicationUser> _userManager;

        public UserHelper(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }

        public async Task<ApplicationUser> GetUserByLoginIdentifer(string userLoginIdentifier)
        {
            ApplicationUser? user = null;
            if (string.IsNullOrEmpty(userLoginIdentifier))
            {
                return user;
            }
            else if (userLoginIdentifier.Contains("@"))
            {
                user = await _userManager.FindByEmailAsync(userLoginIdentifier);
            }
            else
            {
                user = await _userManager.Users.FirstOrDefaultAsync(u => u.UserName == userLoginIdentifier);

            }
            return user;
        }
    }
}
