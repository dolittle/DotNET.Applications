using System;
using Dolittle.Logging;

namespace Dolittle.Build
{
    internal class NullLogAppender : ILogAppender
    {
        public void Append(string filePath, int lineNumber, string member, LogLevel level, string message, Exception exception = null)
        {
            
        }
    }
}
