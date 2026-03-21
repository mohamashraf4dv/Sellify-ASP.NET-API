namespace Sellify.Application.Contracts.Repositories
{
    public interface IUserRepository
    {
        public Task<GenericResultDTO> GetUserInformationByRefreshToken(string refreshToken);

    }
}
