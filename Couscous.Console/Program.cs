using System;
using Couscous.Logging;
using Microsoft.Extensions.DependencyInjection;

namespace Couscous.Console
{
    internal static class Program
    {
        private static DependencyProvider _diProvider;
        private static ILogger _logger;
        
        private static void Main()
        {
            AppDomain.CurrentDomain.UnhandledException += CurrentDomain_UnhandledException;

            _diProvider = new DependencyProvider();
            _diProvider.Load();

            var serviceProvider = _diProvider.BuildServiceProvider();
            
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