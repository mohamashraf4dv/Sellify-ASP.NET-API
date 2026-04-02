

using Sellify.Application.Features.Products.Query.GetAllProducts;

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

        public async Task<IReadOnlyList<GetAllProductsDTO>> GetAllAsync(int pageNumber =1 ,int take=11)
        {
            var skip = (pageNumber - 1) * take;

            var sql = @"SELECT p.Id, Name as ProductName ,Price, Stock , pimg.Url as ProductImg, p.TotalSold,
                        s.ImageURL, CONCAT(s.FirstName,' ',s.LastName) as SellerFullName , AverageReviews.AverageScore FROM Products p 
                        INNER JOIN ProductImage pimg ON pimg.Id = ThumbnailId 
                        INNER JOIN AspNetUsers s ON s.Id = p.SellerId
                        INNER JOIN (SELECT AVG(Score) as AverageScore , ProductID FROM Reviews GROUP BY ProductId) AS AverageReviews 
                        on AverageReviews.ProductId = p.Id 
                        ORDER BY p.CreatedAt DESC OFFSET @skip ROWS FETCH NEXT @take ROWS ONLY;";

            using var connection = _dapperContext.Connection;
            IEnumerable<GetAllProductsDTO> products = await connection.QueryAsync<GetAllProductsDTO>(sql, new {skip,take});
            return products.ToList().AsReadOnly();
        }

        public async Task<IReadOnlyList<Product>> GetProductsBySellerId(string sellerId)
        {
           var products = await _efContext.Products.Where(p=> p.SellerId==sellerId).AsNoTracking().ToListAsync();
            return products;
        }

    }
}
