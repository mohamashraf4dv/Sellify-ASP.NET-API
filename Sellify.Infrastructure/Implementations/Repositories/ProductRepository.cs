
using Dapper;
using Sellify.Application.Contracts.RepoBehavior;

namespace Sellify.Infrastructure.Implementations.Repositories
{
    public class ProductRepository : GenericRepository<Product>,IGetterEntity<Product>
    {
        private readonly SellifyMicrosoftSqlContext _efContext;
        private readonly SellifyDapperContext _dapperContext;

        public ProductRepository(SellifyMicrosoftSqlContext efContext , SellifyDapperContext dapperContext) : base(efContext)
        {
            this._efContext = efContext;
            this._dapperContext = dapperContext;
        }

        public async Task<Product?> GetAsync(string id)
        {
            var sql = "SELECT * FROM Products WHERE id= @id;";
            using var connection = _dapperContext.Connection;
            Product product = await connection.QuerySingleOrDefaultAsync<Product>(sql, new { id });
            return product;
        }

        public async Task<IReadOnlyList<Product>> GetAllAsync()
        {
            var sql = "SELECT * FROM Products";
            using var connection = _dapperContext.Connection;
            IEnumerable<Product> products = await connection.QueryAsync<Product>(sql);
            return products.ToList().AsReadOnly();
        }
    }
}
