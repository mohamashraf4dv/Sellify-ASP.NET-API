
using Sellify.Application.Global;

namespace Sellify.Infrastructure.Services
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _productRepository;

        public ProductService(IProductRepository productRepository)
        {
            this._productRepository = productRepository;
        }
        public async Task<GenericResultDTO> GetProductsAsync(int pageNumber =1, int take=11)
        {
           var products = await _productRepository.GetAllAsync( pageNumber,  take);
            if (products.Count == 0)
            {
                var errors = new Dictionary<string, HashSet<string>>();
                errors["Products"] = new HashSet<string>();
                errors["Products"].Add("There are no products");
                return new GenericResultDTO(null, 404, errors);
            }
            bool isNext = products.Count > take-1;

            return new GenericResultDTO(new { products,isNext }, 200);
        }
    }
}
