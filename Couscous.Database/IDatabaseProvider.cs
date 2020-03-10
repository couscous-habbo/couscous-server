namespace Couscous.Database
{
    public interface IDatabaseProvider
    {
        DatabaseConnection GetConnection();
        bool IsConnected();
    }
}