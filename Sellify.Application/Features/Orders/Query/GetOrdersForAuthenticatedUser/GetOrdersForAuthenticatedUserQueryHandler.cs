
namespace Sellify.Application.Features.Orders.Query.GetOrdersForAuthenticatedUser
{
    public sealed class GetOrdersForAuthenticatedUserQueryHandler : IRequestHandler<GetOrdersForAuthenticatedUserQuery, GenericResultDTO>
    {
        private readonly IGenericRepositoryWithNoSoftDeleteAndUpdate<OrderItem> _orderItemRepository;

        public GetOrdersForAuthenticatedUserQueryHandler(IGenericRepositoryWithNoSoftDeleteAndUpdate<OrderItem> orderItemRepository)
        {
            this._orderItemRepository = orderItemRepository;
        }
        public async Task<GenericResultDTO> Handle(GetOrdersForAuthenticatedUserQuery request, CancellationToken cancellationToken)
        {
            /// This might feels complex but the explanation is as below
            /// 1)Filtering by UserId -> 2) groups by OrderId -> 3) project each OrderId | Items
            var result = await _orderItemRepository.GetAll(oi => oi.Order.BuyerId == request.UserId
                 , oi => oi.OrderId,
                oi => new {
                    OrderId= oi.Key,
                    Items = oi.Select(s=> new GetOrdersForAuthenticatedUserDTO(s.ProductId,s.Product.Name, s.Quantity, s.Price, s.Product.ThumbnailSource ?? "")),
                    TotalAmount= oi.Sum(p=> p.Quantity*p.Price) ,
                    Date= oi.FirstOrDefault().Order.CreatedAt
                });
            return new GenericResultDTO(result,200);
        }
    }
}
