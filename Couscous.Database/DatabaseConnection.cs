using MySql.Data.MySqlClient;

namespace Couscous.Database
{
    public class DatabaseConnection
    {
        private readonly MySqlConnection _connection;
        private readonly MySqlCommand _command;

        public DatabaseConnection(MySqlConnection connection, MySqlCommand command)
        {
            _connection = connection;    
            _command = command;
            
            _connection.Open();
        }
    }
}