using System.Data;
using System.Data.SqlClient;

namespace Sprout.Exam.DataAccess.Repositories.DbContexts
{
    public interface IDapperContext
    {
        public IDbConnection CreateConnection();
    }
    public class DapperContext : IDapperContext
    {
        protected readonly string connectionString;

        public DapperContext(string _connectionString)
        {
            connectionString = _connectionString;
        }

        public IDbConnection CreateConnection()
        {
            return new SqlConnection(connectionString);
        }
    }
}
