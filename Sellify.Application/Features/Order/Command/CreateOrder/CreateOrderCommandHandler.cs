using Sellify.Application.Contracts;
using Sellify.Application.Contracts.Repositories;

namespace Sellify.Application.Features.Order.Command.CreateOrder
{
    public class CreateOrderCommandHandler : IRequestHandler<CreateOrderCommand, GenericResultDTO>
    {
        private readonly IProductRepository _productRepository;
        private readonly IUnitOfWork _unitOfWork;

        public CreateOrderCommandHandler(IProductRepository productRepository , IUnitOfWork unitOfWork)
        {
            this._productRepository = productRepository;
            this._unitOfWork = unitOfWork;
        }
        public async Task<GenericResultDTO> Handle(CreateOrderCommand request, CancellationToken cancellationToken)
        {
            var productsFromDB = _productRepository.GetAllQueryable(p => request.ProductsIds.Contains(p.Id)).ToList();
            foreach (var product in productsFromDB)
            {
                product.BeginSellingTransaction(request.OrderDTO[product.Id].Quantity);
                product.RowVersion = Guid.NewGuid();
            }

            await _unitOfWork.SaveChangesAsync();
            return new GenericResultDTO(null, 200);
        }
    }
}
