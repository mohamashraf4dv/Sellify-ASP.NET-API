using Sellify.Application.Contracts;
using Sellify.Application.Contracts.Repositories;
using Sellify.Domain.Enums;

namespace Sellify.Application.Features.Products.Command.UserWishlistsProduct.Command.NewWishlist
{
    public class NewWishlistCommandHandler : IRequestHandler<NewWishlistCommand, GenericResultDTO<WishlistStatus>>
    {
        private readonly IGenericRepositoryWithNoSoftDeleteAndUpdate<UserWishlistProduct> _userWishlistProductRepo;
        private readonly IUnitOfWork _unitOfWork;

        public NewWishlistCommandHandler(IGenericRepositoryWithNoSoftDeleteAndUpdate<UserWishlistProduct> userWishlistProductRepo, IUnitOfWork unitOfWork)
        {
            this._userWishlistProductRepo = userWishlistProductRepo;
            this._unitOfWork = unitOfWork;
        }
        public async Task<GenericResultDTO<WishlistStatus>> Handle(NewWishlistCommand request, CancellationToken cancellationToken)
        {
            var newWishList = new UserWishlistProduct() { ProductId = request.ProductId ,UserId=request.UserId};
           await _userWishlistProductRepo.CreateAsync(newWishList, cancellationToken);
            var result = await _unitOfWork.SaveChangesAsync();
            if (result > 0) 
                return new GenericResultDTO<WishlistStatus>(WishlistStatus.Added, 201);

            return new GenericResultDTO<WishlistStatus>(WishlistStatus.InvalidRequest, 400);
        }
    }
}
