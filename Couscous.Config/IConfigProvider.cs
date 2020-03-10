namespace Couscous.Config
{
    public interface IConfigProvider
    {
        void Load(string path);
        string GetValueFromKey(string key);
    }
}