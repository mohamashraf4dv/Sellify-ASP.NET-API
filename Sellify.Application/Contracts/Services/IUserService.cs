
namespace Sellify.Application.Contracts.Services
{
    public interface IUserService
    {
        public GenericResultDTO ApplyToRoleSeller(string applicationUserId);
        public GenericResultDTO IsInRoleAsync(string refreshToken);

    }
}
