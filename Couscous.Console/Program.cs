namespace Couscous.Console
{
    internal static class Program
    {
        private static void Main()
        {
            var dependencyProvider = new DependencyProvider();
            dependencyProvider.Register();

            while (true)
            {
                System.Console.ReadLine(); 
            }
        }
    }
}