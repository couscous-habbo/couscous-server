using System;
using Couscous.Logging.Implementations;

namespace Couscous.Logging
{
    public class LogFactory
    {
        public ILogger GetLoggerForType(Type type)
        {
            return new HybridLogger(new ConsoleLogger(), new FileLogger(type));
        }
    }
}