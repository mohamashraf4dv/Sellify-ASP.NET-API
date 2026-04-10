
namespace Sellify.Infrastructure.Implementations
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly SellifyMicrosoftSqlContext _context;
        private IGenericRepositoryWithNoDeleteAndUpdate<Order>? _order= null;
        private IGenericRepositoryWithNoDeleteAndUpdate<OrderItem>? _orderItem = null;
        public UnitOfWork(SellifyMicrosoftSqlContext context)
        {
            this._context = context;
        }

        public IGenericRepositoryWithNoDeleteAndUpdate<Order> Order
        {
            get
            {
                _order ??= new GenericRepositoryWithNoDeleteAndUpdate<Order>(_context);
                return _order;
            }
        }

        public IGenericRepositoryWithNoDeleteAndUpdate<OrderItem> OrderItem
        {
            get
            {
                _orderItem ??= new GenericRepositoryWithNoDeleteAndUpdate<OrderItem>(_context);
                return _orderItem;
            }
        }

        public async Task<int> SaveChangesAsync()
        {
            //var tracked = _context.ChangeTracker.Entries<Product>().Select(p=> new {p.Entity.Id, p.Entity.RowVersion ,Original = p.OriginalValues["RowVersion"] }).ToList();
            return await _context.SaveChangesAsync();

        }
    }
}
