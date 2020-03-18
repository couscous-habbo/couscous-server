namespace Couscous.Database
{
    public abstract class DatabaseAccessObject
    {
        private readonly IDatabaseProvider _databaseProvider;
        
        protected DatabaseAccessObject(IDatabaseProvider databaseProvider)
        {
            _databaseProvider = databaseProvider;
        }

        protected DatabaseConnection GetConnection()
        {
            return _databaseProvider.GetConnection();
        }
    }
}