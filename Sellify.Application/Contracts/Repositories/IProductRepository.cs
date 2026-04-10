using Sellify.Application.Features.Products.Query.GetAllProducts;
using Sellify.Application.Features.Seller.Query.GetBySellerIdProducts;

namespace Sellify.Application.Contracts.Repositories
{
     public interface IProductRepository: IGenericRepository<Product> 
    {
        Task<IReadOnlyList<GetAllProductsDTO>> GetAllAsync(int pageNumber = 1, int take = 11);
        //Task<IReadOnlyList<Product>> GetProductsBySellerId(string sellerId);
        Task<IReadOnlyList<GetBySellerIdProductsQueryDTO>> GetProductsBySellerId(string sellerId);
        public Task UpdateRangeSpecificallyStockPriceNameAsync(IReadOnlyList<Product> products);
        Task UpdateRowVersion(Product product);
    }
}
