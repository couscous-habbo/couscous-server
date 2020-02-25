namespace Couscous.Database
{
    public abstract  class DatabaseAccessObject
    {
        private readonly DatabaseProvider _databaseProvider;
        
        protected DatabaseAccessObject(DatabaseProvider databaseProvider)
        {
            _databaseProvider = databaseProvider;
        }

        protected DatabaseConnection GetConnection()
        {
            return _databaseProvider.GetConnection();
        }
    }
}