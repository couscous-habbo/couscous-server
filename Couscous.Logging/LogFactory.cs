using System;
using Couscous.Logging.Implementations;

namespace Couscous.Logging
{
    public class LogFactory
    {
        public static ILogger GetLoggerForType()
        {
            return new HybridLogger(
                new ConsoleLogger(), 
                new FileLogger()
            );
        }
    }
}