namespace Sellify.Infrastructure.Implementations.Repositories
{
    public class OrderItemRepository:GenericRepositoryWithNoSoftDeleteAndUpdate<OrderItem>
    {
        private readonly SellifyMicrosoftSqlContext _context;

        public OrderItemRepository(SellifyMicrosoftSqlContext context) : base(context)
        {
            this._context = context;
        }
    }
}
