using System;
using System.Data;
using System.Threading.Tasks;
using Couscous.Logging;
using MySql.Data.MySqlClient;

namespace Couscous.Database
{
    public class DatabaseConnection : IDisposable
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

        public async Task ExecuteQueryAsync()
        {
            try
            {
                await _command.ExecuteNonQueryAsync();
            }
            catch (MySqlException me)
            {
                _logger.Exception(me);
            }
        }

        public async Task<DataTable> ExecuteTableAsync()
        {
            var dataTable = new DataTable();
            
            try
            {
                using var adapter = new MySqlDataAdapter(_command);
                await adapter.FillAsync(dataTable);
            }
            catch (MySqlException me)
            {
                _logger.Exception(me);
            }

            return dataTable;
        }

        public async Task<DataRow> ExecuteRowAsync()
        {
            DataRow dataRow = null;
            
            try
            {
                var dataSet = new DataSet();
                
                using (var adapter = new MySqlDataAdapter(_command))
                {
                    await adapter.FillAsync(dataSet);
                }
                
                if (dataSet.Tables.Count > 0 && dataSet.Tables[0].Rows.Count == 1)
                {
                    dataRow = dataSet.Tables[0].Rows[0];
                }
            }
            catch (MySqlException me)
            {
                _logger.Exception(me);
            }

            return dataRow;
        }

        public void AddParameter(string name, object value)
        {
            _command.Parameters.AddWithValue(name, value);
        }

        public void Dispose()
        {
            if (_connection.State != ConnectionState.Closed)
            {
                _connection.Close();
            }
            
            _connection.Dispose();
            _command.Dispose();
        }
    }
}