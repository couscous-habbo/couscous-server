using Couscous.Logging.Implementations;

namespace Couscous.Logging
{
    public class LogFactory
    {
        public ILogger GetLogger()
        {
            return new HybridLogger(
                new ConsoleLogger(), 
                new FileLogger()
            );
        }
    }
}