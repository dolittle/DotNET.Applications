using System;
using Microsoft.Extensions.Logging;

namespace Dolittle.Build
{
    internal class NullDisposable : IDisposable
    {
        public void Dispose()
        {
        }
    }
    internal class NullLogger : Microsoft.Extensions.Logging.ILogger
    {
        public IDisposable BeginScope<TState>(TState state) => new NullDisposable();
        

        public bool IsEnabled(LogLevel logLevel) => true;
        

        public void Log<TState>(LogLevel logLevel, EventId eventId, TState state, Exception exception, Func<TState, Exception, string> formatter)
        {
        }
    }
}