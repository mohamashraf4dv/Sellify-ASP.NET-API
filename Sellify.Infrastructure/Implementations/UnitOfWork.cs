
namespace Sellify.Infrastructure.Implementations
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly SellifyMicrosoftSqlContext _context;

        public UnitOfWork(SellifyMicrosoftSqlContext context)
        {
            this._context = context;
        }
        public async Task<int> SaveChangesAsync()
        {
            return await _context.SaveChangesAsync();
        }
    }
}
