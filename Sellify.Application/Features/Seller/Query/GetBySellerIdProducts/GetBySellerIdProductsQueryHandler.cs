using AutoMapper;
using Sellify.Application.Contracts.Repositories;

namespace Sellify.Application.Features.Seller.Query.GetBySellerIdProducts
{
    public class GetBySellerIdProductsQueryHandler : IRequestHandler<GetBySellerIdProductsQuery, GenericResultDTO<IReadOnlyList<GetBySellerIdProductsQueryDTO>>>
    {
        private readonly IProductRepository _productRepository;
        private readonly IMapper _mapper;

        public GetBySellerIdProductsQueryHandler(IProductRepository productRepository, IMapper mapper)
        {
            this._productRepository = productRepository;
            this._mapper = mapper;
        }
        public async Task<GenericResultDTO<IReadOnlyList<GetBySellerIdProductsQueryDTO>>> Handle(GetBySellerIdProductsQuery request, CancellationToken cancellationToken)
        {
           var products = await _productRepository.GetProductsBySellerId(request.SellerId);
            if (products is null || !products.Any())
                return new GenericResultDTO<IReadOnlyList<GetBySellerIdProductsQueryDTO>>(null, 404);
            var productsDTO = _mapper.Map<IReadOnlyList<GetBySellerIdProductsQueryDTO>>(products);
            return new GenericResultDTO<IReadOnlyList<GetBySellerIdProductsQueryDTO>>(productsDTO, 200);
        }
    }
}
