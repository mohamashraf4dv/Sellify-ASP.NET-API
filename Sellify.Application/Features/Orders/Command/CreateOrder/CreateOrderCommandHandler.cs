using Sellify.Application.Contracts;
using Sellify.Application.Contracts.Repositories;
namespace Sellify.Application.Features.Orders.Command.CreateOrder
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
            var order = new Order() { BuyerId = request.BuyerId };
            foreach (var product in productsFromDB)
            {
                long orderItemQuantity = request.OrderDTO?[product.Id]?.Quantity ?? 0;
                if (orderItemQuantity == 0)
                    return new GenericResultDTO(null, 400);

                product.BeginSellingTransaction(orderItemQuantity);
                product.OrderItems.Add(new OrderItem { Price = product.Price, Quantity = orderItemQuantity, ProductId = product.Id, Order = order });
                product.RowVersion = Guid.NewGuid();

            }

            await _unitOfWork.SaveChangesAsync();
            return new GenericResultDTO(null, 200);
        }
    }
}
