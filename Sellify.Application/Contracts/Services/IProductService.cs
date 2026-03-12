namespace Sellify.Application.Contracts.Services
{
    public interface IProductService
    {
        Task<GenericResultDTO> GetProductsAsync(int pageNumber = 1, int take = 11);
    }
}
