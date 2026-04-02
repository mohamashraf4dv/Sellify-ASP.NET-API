namespace Sellify.Application.Features.Products.Query.GetAllProducts
{
    public record GetAllProductsWithNextOptionDTO(
        IReadOnlyList<GetAllProductsDTO> Products,
        bool IsNext
        );
    
}
