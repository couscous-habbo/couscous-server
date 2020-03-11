using System;
using Couscous.Logging;
using Microsoft.Extensions.DependencyInjection;

namespace Couscous.Console
{
    internal static class Program
    {
        private static ILogger _logger;
        
        private static void Main()
        {
            AppDomain.CurrentDomain.UnhandledException += CurrentDomain_UnhandledException;

            var dependencyProvider = new DependencyProvider();
            dependencyProvider.Load();

            var serviceProvider = dependencyProvider.BuildServiceProvider();
            
            _logger = serviceProvider.GetRequiredService<LogFactory>().GetLogger();

            serviceProvider.GetService<Server>().Start();
            
            while (true)
            {
                System.Console.ReadLine(); 
            }
        }
        
        private static void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            _logger?.Exception((e.ExceptionObject as Exception));
        }
    }
}