namespace Sellify.Application.Features.Products.Query.GetAllProducts
{
    public record GetAllProductsQuery (int PageNumber, int Take) : IRequest<GenericResultDTO>;
}
