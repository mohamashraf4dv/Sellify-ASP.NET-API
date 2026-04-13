namespace Sellify.Infrastructure.ApplicationContext
{
    public class SellifyDapperContext : IDisposable
    {
        private readonly IConfiguration _configuration;
        private IDbConnection? _connection;
        public SellifyDapperContext(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public IDbConnection Connection 
        { 
            get {
                _connection ??= new SqlConnection(_configuration.GetConnectionString("DefaultConnection"));
                return _connection;
            } 
        }

        public void Dispose()
        {
            _connection?.Dispose();
        }
    }
}
