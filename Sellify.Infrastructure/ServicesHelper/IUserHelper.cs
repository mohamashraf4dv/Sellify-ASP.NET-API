namespace Sellify.Infrastructure.ServicesHelper
{
    public interface IUserHelper
    {
        public Task<ApplicationUser> GetUserByLoginIdentifer(string userLoginIdentifier);
    }
}
