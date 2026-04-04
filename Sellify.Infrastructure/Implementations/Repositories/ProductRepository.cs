

using Sellify.Application.Features.Products.Command.SellerUpdateProducts;
using Sellify.Application.Features.Products.Query.GetAllProducts;
using Sellify.Application.Features.Seller.Query.GetBySellerIdProducts;

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

            var sql = @"SELECT p.Id, Name as ProductName ,Price, Stock ,p.ThumbnailSource, p.TotalSold,
                         CONCAT(s.FirstName,' ',s.LastName) as SellerFullName , s.ImageURL as SellerImage , CreatedAt , AverageReviews.AverageScore FROM Products p 
                        INNER JOIN AspNetUsers s ON s.Id = p.SellerId
                        LEFT JOIN (SELECT AVG(Score) as AverageScore , ProductID FROM Reviews GROUP BY ProductId) AS AverageReviews 
                        on AverageReviews.ProductId = p.Id 
                        ORDER BY p.CreatedAt DESC OFFSET @skip ROWS FETCH NEXT @take ROWS ONLY;";
            using var connection = _dapperContext.Connection;
            IEnumerable<GetAllProductsDTO> products = await connection.QueryAsync<GetAllProductsDTO>(sql, new {skip,take});
            return products.ToList().AsReadOnly();
        }

        #region GetProductsBySellerId(sid) that returns product list 
        //public async Task<IReadOnlyList<Product>> GetProductsBySellerId(string sellerId)
        //{
        //   var products = 
        //        await _efContext.Products
        //                        .AsNoTracking()
        //                        .AsSplitQuery()
        //                        .Include(p=> p.ProductImages)
        //                        .Where(p=> p.SellerId==sellerId)
        //                        .ToListAsync();
        //    return products;
        //}

        #endregion

        //this violates the repository pattern but it is more efficient than the previous one because it only selects the necessary fields and not the whole product entity with all its navigation properties
        public async Task<IReadOnlyList<GetBySellerIdProductsQueryDTO>> GetProductsBySellerId(string sellerId)
        {
            var products =
                 await _efContext.Products
                                 .AsNoTracking()
                                 .AsSplitQuery()
                                 .Where(p => p.SellerId == sellerId)
                                 .Select(p => new GetBySellerIdProductsQueryDTO
                                 (
                                     p.Id,
                                     p.Name,
                                     p.Price,
                                     p.Stock,
                                     p.ProductImages.Select(pi => pi.Url).ToList(),
                                     p.ThumbnailSource
                                 )).ToListAsync();
                                 
            return products;
        }

        public Task UpdateRangeSpecificallyStockPriceNameAsync(IReadOnlyList<Product> products)
        {
            _efContext.AttachRange(products);
            foreach (var product in products)
            {
                _efContext.Entry(product).Property(p => p.Stock).IsModified = true;
                _efContext.Entry(product).Property(p => p.Price).IsModified = true;
                _efContext.Entry(product).Property(p => p.Name).IsModified = true;
                _efContext.Entry(product).Property(p => p.LastUpdatedAt).CurrentValue = DateTime.UtcNow;
                _efContext.Entry(product).Property(p => p.LastUpdatedAt).IsModified = true;

            }
            return Task.CompletedTask;
        }
    }
}
