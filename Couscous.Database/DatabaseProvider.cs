using Couscous.Logging;
using MySql.Data.MySqlClient;

namespace Couscous.Database
{
    public class DatabaseProvider : IDatabaseProvider
    {
        private readonly string _connectionString;
        
        public DatabaseProvider(string connectionString)
        {
            _connectionString = connectionString;
        }
        
        public DatabaseConnection GetConnection()
        {
            var connection = new MySqlConnection(_connectionString);
            var command = connection.CreateCommand();
            
            return new DatabaseConnection(LogFactory.GetLogger(typeof(DatabaseConnection)), connection, command);
        }

        public bool IsConnected()
        {
            try
            {
                GetConnection();
                return true;
            }
            catch (MySqlException)
            {
                return false;
            }
        }
    }
}