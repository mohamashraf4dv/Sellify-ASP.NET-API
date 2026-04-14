namespace Sellify.Infrastructure.Implementations.Repositories
{
    internal class OrderRepository :GenericRepositoryWithNoSoftDeleteAndUpdate<Order>, IOrderRepository
    {
        private readonly SellifyMicrosoftSqlContext _context;

        public OrderRepository(SellifyMicrosoftSqlContext context) : base(context)
        {
            this._context = context;
        }



    }
}
