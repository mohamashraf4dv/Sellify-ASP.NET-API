using Sellify.Application.Contracts;
using Sellify.Application.Contracts.Repositories;

namespace Sellify.Application.Features.Products.Command.SellerAddProduct
{
    public class SellerAddProductCommandHandler : IRequestHandler<SellerAddProductCommand, GenericResultDTO>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IProductRepository _productRepository;

        public SellerAddProductCommandHandler(IUnitOfWork unitOfWork,IProductRepository productRepository)
        {
            this._unitOfWork = unitOfWork;
            this._productRepository = productRepository;
        }
        public async Task<GenericResultDTO> Handle(SellerAddProductCommand request, CancellationToken cancellationToken)
        {
            //_productRepository.CreateAsync()
            await _unitOfWork.SaveChangesAsync();
            throw new NotImplementedException();
        }
    }
}
