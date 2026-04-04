using AutoMapper;
using Sellify.Application.Contracts;
using Sellify.Application.Contracts.Repositories;

namespace Sellify.Application.Features.Products.Command.SellerUpdateProducts
{
    public class SellerUpdateProductsCommandHandler : IRequestHandler<SellerUpdateProductsCommand, GenericResultDTO>
    {
        private readonly IMapper _mapper;
        private readonly IProductRepository _productRepository;
        private readonly IUnitOfWork _unitOfWork;

        public SellerUpdateProductsCommandHandler(IMapper mapper,IProductRepository productRepository, IUnitOfWork unitOfWork)
        {
            this._mapper = mapper;
            this._productRepository = productRepository;
            this._unitOfWork = unitOfWork;
        }
        public async Task<GenericResultDTO> Handle(SellerUpdateProductsCommand request, CancellationToken cancellationToken)
        {
            var products = _mapper.Map<IReadOnlyList<SellerUpdateProductsDTO>, IReadOnlyList<Product>>(request.Products);
           await _productRepository.UpdateRangeSpecificallyStockPriceNameAsync(products);
           var result = await _unitOfWork.SaveChangesAsync();

            if (result <= 0)
            {
                var errorsKeyValues = new Dictionary<string, HashSet<string>>
                    {
                        { "Products", new HashSet<string> { "Failed to update products. something wrong happened" } }
                    };
                return new GenericResultDTO( null, 500,errorsKeyValues);

            }
            return new GenericResultDTO(null, 200);

        }
    }
}
