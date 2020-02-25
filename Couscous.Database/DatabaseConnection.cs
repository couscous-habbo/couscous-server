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
                // @TODO - Log some kind of exception here?
            }
        }
    }
}