using Sellify.Application.Contracts.Repositories;

namespace Sellify.Application.Features.Products.Query.GetAllProducts
{
    public class GetAllProductsQueryHandler : IRequestHandler<GetAllProductsQuery, GenericResultDTO<GetAllProductsWithNextOptionDTO>>
    {
        private readonly IProductRepository _productRepository;

        public GetAllProductsQueryHandler( IProductRepository productRepository)
        {
            this._productRepository = productRepository;
        }
        public async Task<GenericResultDTO<GetAllProductsWithNextOptionDTO>> Handle(GetAllProductsQuery request, CancellationToken cancellationToken)
        {
            if (request.Take > 25)
            {
                var errors = new Dictionary<string, HashSet<string>>();
                errors["Products"] = new HashSet<string>();
                errors["Products"].Add("You cannot show more than 25 products at once");
                return new GenericResultDTO<GetAllProductsWithNextOptionDTO>(null, 400, errors);
            }
            var products = await _productRepository.GetAllAsync(request.PageNumber, request.Take);
            if (products.Count == 0)
            {
                var errors = new Dictionary<string, HashSet<string>>();
                errors["Products"] = new HashSet<string>();
                errors["Products"].Add("There are no products");
                return new GenericResultDTO<GetAllProductsWithNextOptionDTO>(null, 404, errors);
            }
            var returnedProducts = products.Take(request.Take - 1).ToList();
            bool isNext = products.Count > returnedProducts.Count;
            return new GenericResultDTO<GetAllProductsWithNextOptionDTO>(new GetAllProductsWithNextOptionDTO(returnedProducts, isNext ), 200);
        }
    }
}
