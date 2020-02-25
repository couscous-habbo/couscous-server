using System;

namespace Couscous.Logging
{
    public interface IPersistLogger
    {
        void Persist(string message, LogLevel level);
        void Persist(Exception exception);
    }
}