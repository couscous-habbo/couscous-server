using System;
using Couscous.Logging.Implementations;

namespace Couscous.Logging
{
    public static class LogFactory
    {
        public static ILogger GetLogger(Type type)
        {
            return new HybridLogger(new ConsoleLogger(), new FileLogger(type));
        }
    }
}