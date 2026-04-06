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
        public Task<GenericResultDTO> Handle(CreateOrderCommand request, CancellationToken cancellationToken)
        {

        }
    }
}
