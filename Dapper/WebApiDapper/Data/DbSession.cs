 
using System.Data;
using System.Data.SqlClient;
 

namespace WebApiDapper.Data
{
    public class DbSession : IDisposable
    {
        public IDbConnection Connection { get;  }
        
        public DbSession(IConfiguration configuration)
        {
            Connection = new SqlConnection(configuration
            .GetConnectionString("DefaultConnection"));
            Connection.Open();
        }
        public void Dispose() => Connection.Dispose();

    }
}