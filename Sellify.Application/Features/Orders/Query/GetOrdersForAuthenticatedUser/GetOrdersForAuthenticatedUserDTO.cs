namespace Sellify.Application.Features.Orders.Query.GetOrdersForAuthenticatedUser
{
    public record GetOrdersForAuthenticatedUserDTO
        (Guid ProductId,string Name, long Quantity , decimal Price , string ProductImage
        );

}
