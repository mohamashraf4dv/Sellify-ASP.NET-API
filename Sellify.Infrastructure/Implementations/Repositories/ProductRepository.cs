

namespace Sellify.Infrastructure.Implementations.Repositories
{
    public class ProductRepository : GenericRepository<Product>,IProductRepository
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
            var sql = "SELECT p.Id, Name ,Price, Stock , pimg.* FROM Products p INNER JOIN ProductImage pimg ON pimg.Id = ThumbnailId WHERE p.Id=@id;";
            using var connection = _dapperContext.Connection;
            Product product = await connection.QuerySingleOrDefaultAsync<Product>(sql, new { id });
            return product;
        }

        public async Task<IReadOnlyList<Product>> GetAllAsync(int pageNumber =1 ,int take=11)
        {
            var skip = (pageNumber - 1) * take;
            var sql = "SELECT p.Id, Name ,Price, Stock , pimg.Url FROM Products p INNER JOIN ProductImage pimg ON pimg.Id = ThumbnailId OFFSET @skip ROWS FETCH @take ROWS ONLY;";
            using var connection = _dapperContext.Connection;
            IEnumerable<Product> products = await connection.QueryAsync<Product>(sql, new {skip,take});
            return products.ToList().AsReadOnly();
        }
    }
}
