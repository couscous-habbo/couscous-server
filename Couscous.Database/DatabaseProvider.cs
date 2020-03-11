using Couscous.Logging;
using MySql.Data.MySqlClient;

namespace Couscous.Database
{
    public class DatabaseProvider : IDatabaseProvider
    {
        private readonly string _connectionString;
        private readonly LogFactory _logFactory;
        
        public DatabaseProvider(string connectionString, LogFactory logFactory)
        {
            _connectionString = connectionString;
            _logFactory = logFactory;
        }
        
        public DatabaseConnection GetConnection()
        {
            var connection = new MySqlConnection(_connectionString);
            var command = connection.CreateCommand();
            
            return new DatabaseConnection(
                _logFactory.GetLoggerForType(typeof(DatabaseConnection)
            ), connection, command);
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