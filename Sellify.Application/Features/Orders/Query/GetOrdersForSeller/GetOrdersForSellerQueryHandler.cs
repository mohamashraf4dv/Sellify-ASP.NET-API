namespace Sellify.Application.Features.Orders.Query.GetOrdersForSeller
{
    public sealed class GetOrdersForSellerQueryHandler : IRequestHandler<GetOrdersForSellerQuery, GenericResultDTO>
    {
        private readonly IGenericRepositoryWithNoSoftDeleteAndUpdate<OrderItem> _orderItemRepository;

        public GetOrdersForSellerQueryHandler(IGenericRepositoryWithNoSoftDeleteAndUpdate<OrderItem> orderItemRepository)
        {
            this._orderItemRepository = orderItemRepository;
        }
        public async Task<GenericResultDTO> Handle(GetOrdersForSellerQuery request, CancellationToken cancellationToken)
        {
            /// This might feels complex but the explanation is as below
            #region In case in future we want to enfore uniqueness per product but the problem will be faced is same order item might have different prices which might cause invalid data sent
            /// 1)Filtering by SellerIdOnProducts -> 2) groups by ProductId -> 3) project each OrderId | Items
            //var result = await _orderItemRepository.GetAll(oi => oi.Product.SellerId == request.UserId
            //     , oi => new { oi.ProductId , oi.Product.ThumbnailSource,oi.Product.Name,},
            //    s => new GetOrdersForSellerDTO(s.FirstOrDefault().OrderId, s.Key.ProductId, s.Key.Name, s.Sum(a=>a.Quantity), s.FirstOrDefault().Price, s.Key.ThumbnailSource ?? "",
            //    s.FirstOrDefault().Order.CreatedAt, s.Sum(p => p.Quantity * p.Price)  )); 
            #endregion

            #region In case if we don't care about projecting per order
            /// 1) filtering by sellerId 2) projection
            //var result = await _orderItemRepository.GetAll(oi => oi.Product.SellerId == request.UserId,
            //    oi => new GetOrdersForSellerDTO(oi.OrderId, oi.ProductId, oi.Product.Name, oi.Quantity, oi.Price, oi.Product.ThumbnailSource ?? "",
            //    oi.Order.CreatedAt, (oi.Quantity * oi.Price))); 
            #endregion

            // 1)Filtering by SellerIdOnProducts-> 2) groups by OrderId-> 3) project each OrderId | Items
            var result = await _orderItemRepository.GetAll(oi => oi.Product.SellerId == request.UserId
                 , oi => oi.OrderId,
                oi => new {
                    OrderId= oi.Key,
                    OrderItems=  oi.Select(i=> new GetOrdersForSellerDTO(i.ProductId, i.Product.Name, i.Quantity, i.Price, i.Product.ThumbnailSource ?? "")),
                    TotalEarned=oi.Sum(i=> i.Quantity*i.Price)
                });

            return new GenericResultDTO(result,200);
        }
    }
}
