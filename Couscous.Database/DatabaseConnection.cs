using Couscous.Logging;
using MySql.Data.MySqlClient;

namespace Couscous.Database
{
    public class DatabaseConnection
    {
        private readonly ILogger _logger;
        private readonly MySqlConnection _connection;
        private readonly MySqlCommand _command;

        public DatabaseConnection(ILogger logger, MySqlConnection connection, MySqlCommand command)
        {
            _logger = logger;
            
            _connection = connection;    
            _command = command;
            
            _connection.Open();
        }

        public void SetQuery(string commandText)
        {
            _command.Parameters.Clear();
            _command.CommandText = commandText;
        }

        public void ExecuteQuery()
        {
            try
            {
                _command.ExecuteNonQuery();
            }
            catch (MySqlException me)
            {
                _logger.Exception(me);
            }
        }
    }
}