
namespace Sellify.Infrastructure.Implementations
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly SellifyMicrosoftSqlContext _context;
        private IGenericRepositoryWithNoSoftDeleteAndUpdate<Order>? _order= null;
        private IGenericRepositoryWithNoSoftDeleteAndUpdate<OrderItem>? _orderItem = null;
        public UnitOfWork(SellifyMicrosoftSqlContext context)
        {
            this._context = context;
        }

        public IGenericRepositoryWithNoSoftDeleteAndUpdate<Order> Order
        {
            get
            {
                _order ??= new GenericRepositoryWithNoSoftDeleteAndUpdate<Order>(_context);
                return _order;
            }
        }

        public IGenericRepositoryWithNoSoftDeleteAndUpdate<OrderItem> OrderItem
        {
            get
            {
                _orderItem ??= new GenericRepositoryWithNoSoftDeleteAndUpdate<OrderItem>(_context);
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
