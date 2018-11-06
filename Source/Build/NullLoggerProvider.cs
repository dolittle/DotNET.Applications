using Microsoft.Extensions.Logging;

namespace Dolittle.Build
{
    internal class NullLoggerProvider : ILoggerProvider
    {
        public Microsoft.Extensions.Logging.ILogger CreateLogger(string categoryName) => new NullLogger();

        public void Dispose()
        {
        }
    }
}