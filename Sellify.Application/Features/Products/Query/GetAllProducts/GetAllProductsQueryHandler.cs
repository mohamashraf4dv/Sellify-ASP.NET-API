namespace Sellify.Application.Features.Products.Query.GetAllProducts
{
    public class GetAllProductsQueryHandler : IRequestHandler<GetAllProductsQuery, GenericResultDTO>
    {
        private readonly IProductService _productService;

        public GetAllProductsQueryHandler(IProductService productService)
        {
            this._productService = productService;
        }
        public async Task<GenericResultDTO> Handle(GetAllProductsQuery request, CancellationToken cancellationToken)
        {
           var result = await _productService.GetProductsAsync(request.PageNumber, request.Take);
            return result;
        }
    }
}
