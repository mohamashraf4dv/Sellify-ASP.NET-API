

using Sellify.Application.Features.Products.Query.GetAllProducts;

namespace Sellify.Application.Contracts.Repositories
{
     public interface IProductRepository: IGenericRepository<Product> 
    {
        Task<IReadOnlyList<GetAllProductsDTO>> GetAllAsync(int pageNumber = 1, int take = 11);
        Task<IReadOnlyList<Product>> GetProductsBySellerId(string sellerId);
    }
}
