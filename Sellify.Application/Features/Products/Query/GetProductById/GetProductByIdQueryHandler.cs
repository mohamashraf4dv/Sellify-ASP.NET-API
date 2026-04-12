using Sellify.Application.Contracts.Repositories;

namespace Sellify.Application.Features.Products.Query.GetProductById
{
    public class GetProductByIdQueryHandler : IRequestHandler<GetProductByIdQuery, GenericResultDTO<GetProductByIdDTO>>
    {
        private readonly IProductRepository _productRepository;

        public GetProductByIdQueryHandler(IProductRepository productRepository)
        {
            this._productRepository = productRepository;
        }
        public async Task<GenericResultDTO<GetProductByIdDTO>> Handle(GetProductByIdQuery request, CancellationToken cancellationToken)
        {
           var product =  await this._productRepository.GetAsync<GetProductByIdDTO>(request.ProductId
               ,p => new GetProductByIdDTO(p.Id, p.ThumbnailSource, p.Price, p.Description, p.Stock, 
                    p.Reviews.Select(e=> new GetProductByIdReviews(e.Description,e.ApplicationUserName,e.Score)).ToList())
                , f => f.Id == request.ProductId);

            return new GenericResultDTO<GetProductByIdDTO>(product,200);
        }
    }
}
