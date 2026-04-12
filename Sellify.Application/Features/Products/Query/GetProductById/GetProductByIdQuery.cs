namespace Sellify.Application.Features.Products.Query.GetProductById
{
    public record GetProductByIdQuery(Guid ProductId):IRequest<GenericResultDTO<GetProductByIdDTO>>;

}
