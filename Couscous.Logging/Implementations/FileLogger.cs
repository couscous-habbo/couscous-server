using System;
using System.Collections.Generic;

namespace Couscous.Logging.Implementations
{
    public class FileLogger : IPersistLogger
    {
        private readonly Type _owner;
            
        public FileLogger(Type owner)
        {
            _owner = owner;
        }
            
        private readonly Dictionary<LogLevel, string> _fileNameForLogType = new Dictionary<LogLevel, string>() {
            {LogLevel.Trace , "trace.log"},
            {LogLevel.Success , "success.log"},
            {LogLevel.Warning ,    "warn.log"},
            {LogLevel.Debug ,   "debug.log"},
            {LogLevel.Error ,   "error.log"},
        };

        public void Persist(string e, LogLevel level)
        {
            // TODO: persist? 
        }

        public void Persist(Exception e)
        {
            Persist(e.ToString(), LogLevel.Error);
        }
    }
}