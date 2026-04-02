namespace Sellify.Infrastructure.Implementations.Repositories
{
    public class SellerRepository : ISellerRepository
    {
        private readonly SellifyMicrosoftSqlContext _sellifyMicrosoftSqlContext;

        public SellerRepository(SellifyMicrosoftSqlContext sellifyMicrosoftSqlContext)
        {
            this._sellifyMicrosoftSqlContext = sellifyMicrosoftSqlContext;
        }
        public async Task CreateAsync(Seller seller)
        {
            _sellifyMicrosoftSqlContext.Sellers.Add(seller);
        }

    }

}
