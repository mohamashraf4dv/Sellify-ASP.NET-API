using Sellify.Application.Contracts;
using Sellify.Application.Contracts.Repositories;

namespace Sellify.Application.Features.Reviews.Commands.CustomerWritesReview
{
    public class CustomerWritesReviewCommandHandler : IRequestHandler<CustomerWritesReviewCommand, GenericResultDTO>
    {
        private readonly IReviewRepository _reviewRepository;
        private readonly IProductRepository _productRepository;
        private readonly IUnitOfWork _unitOfWork;

        public CustomerWritesReviewCommandHandler(IReviewRepository reviewRepository, IProductRepository productRepository ,IUnitOfWork unitOfWork)
        {
            this._reviewRepository = reviewRepository;
            this._productRepository = productRepository;
            this._unitOfWork = unitOfWork;
        }
        public async Task<GenericResultDTO> Handle(CustomerWritesReviewCommand request, CancellationToken cancellationToken)
        {
            bool productExists = await _productRepository.IsExist(p=> p.Id== request.ProductId);
            if(!productExists)
                return new GenericResultDTO(null, 400, new Dictionary<string, HashSet<string>>() { ["Product"] = new HashSet<string>() { "Product Isnot Exist , Action cannot be placed" } });

            var review = new Review()
            {
                ApplicationUserId = request.UserId,
                ApplicationUserName = request.UserFullName,
                Description = request.Description,
                ProductId = request.ProductId,
                Score=request.Score
            };
              var result =  await _reviewRepository.CreateIfNotExist(review);
            if (result == 0)
                return new GenericResultDTO(null, 400,new Dictionary<string, HashSet<string>>() { ["User"]= new HashSet<string>() { "User Already Reviewed this Product , can't place more than one review per product"} });

            await _unitOfWork.SaveChangesAsync();
            return new GenericResultDTO(null, 201);
        }
    }
}
