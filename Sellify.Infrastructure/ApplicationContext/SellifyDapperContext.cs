
using Microsoft.Data.SqlClient;
using System.Data;

namespace Sellify.Infrastructure.ApplicationContext
{
    public class SellifyDapperContext
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
    }
}
