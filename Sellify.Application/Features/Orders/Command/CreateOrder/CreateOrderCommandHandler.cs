using Sellify.Application.Contracts;
using Sellify.Application.Contracts.Repositories;
namespace Sellify.Application.Features.Orders.Command.CreateOrder
{
    public class CreateOrderCommandHandler : IRequestHandler<CreateOrderCommand, GenericResultDTO>
    {
        private readonly IProductRepository _productRepository;
        private readonly IPaymentService _paymentService;
        private readonly IUnitOfWork _unitOfWork;

        public CreateOrderCommandHandler(IProductRepository productRepository , IPaymentService paymentService ,IUnitOfWork unitOfWork)
        {
            this._productRepository = productRepository;
            this._paymentService = paymentService;
            this._unitOfWork = unitOfWork;
        }
        public async Task<GenericResultDTO> Handle(CreateOrderCommand request, CancellationToken cancellationToken)
        {
            var stripeReturnedDictionary =  await _paymentService.GetOrderBySessionId(request.SessionId);
            var productsFromDb = _productRepository.GetAllQueryable(p => stripeReturnedDictionary.Keys.Contains(p.Id.ToString())).ToHashSet();
            //var productsFromDb = _unitOfWork.ProductRepository.GetAllQueryable(p => stripeReturnedDictionary.Keys.Contains(p.Id.ToString())).ToList();

            Order order = new Order() { BuyerId= stripeReturnedDictionary.Values.FirstOrDefault().BuyerId };
            await _unitOfWork.Order.CreateAsync(order);

            foreach (var product in productsFromDb)
            {
                var orderItemQuantity = stripeReturnedDictionary[product.Id.ToString()].Quantity ?? 0;
                if (orderItemQuantity == 0)
                    return new GenericResultDTO(null, 400, new Dictionary<string, HashSet<string>> { ["Quantity"] = new HashSet<string> { "Quantity Cannot be 0" } });

                
                product.BeginSellingTransaction(orderItemQuantity);
                var orderItem = new OrderItem { Price = product.Price, Quantity = orderItemQuantity, Order = order };
                await _unitOfWork.OrderItem.CreateAsync(orderItem);
                product.OrderItems.Add(orderItem);

                await _productRepository.UpdateRowVersion(product);
            }

            await _unitOfWork.SaveChangesAsync();

            return new GenericResultDTO(null, 200);
        }
    }
}
