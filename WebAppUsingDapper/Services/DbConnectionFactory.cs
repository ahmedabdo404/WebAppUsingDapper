using Microsoft.Data.SqlClient;

namespace WebAppUsingDapper.Services
{
    public class DbConnectionFactory
    {
        private readonly string connectionString;
        public DbConnectionFactory(string connectionString)
        {
            this.connectionString = connectionString;
        }

        public SqlConnection Create() => new SqlConnection(connectionString);
    }
}
