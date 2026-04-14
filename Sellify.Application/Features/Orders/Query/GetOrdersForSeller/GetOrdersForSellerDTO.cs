namespace Sellify.Application.Features.Orders.Query.GetOrdersForSeller
{
    //public record GetOrdersForSellerDTO(Guid OrderId,Guid ProductId, string Name, long Quantity, decimal Price, string ProductImage,DateTime OrderDate,decimal TotalEarning);
    #region In case we used one of the first two options
    public record GetOrdersForSellerDTO
        (Guid ProductId, string Name, long Quantity, decimal Price, string ProductImage
        );
    #endregion
}
