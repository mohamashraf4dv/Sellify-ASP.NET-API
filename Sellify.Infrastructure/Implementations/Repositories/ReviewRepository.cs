namespace Sellify.Infrastructure.Implementations.Repositories
{
    public class ReviewRepository : IReviewRepository
    {
        private readonly SellifyMicrosoftSqlContext _context;

        public ReviewRepository(SellifyMicrosoftSqlContext context)
        {
            this._context = context;
        }
        public async Task<int> CreateIfNotExist(Review review)
        {
           var isAlreadyReviewed = await _context.Reviews.AnyAsync(r=> r.ApplicationUserId == review.ApplicationUserId && r.ProductId == review.ProductId);
            if (isAlreadyReviewed)
                return 0;
            await _context.Reviews.AddAsync(review);
            return 1;
        }
    }
}
