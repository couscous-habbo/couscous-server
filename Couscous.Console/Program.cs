namespace Couscous.Console
{
    internal static class Program
    {
        private static void Main()
        {
            var dependencyProvider = new DependencyProvider();
            dependencyProvider.Load();

            while (true)
            {
                System.Console.ReadLine(); 
            }
        }
    }
}