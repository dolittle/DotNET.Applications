

using System;
using System.IO;
using System.Runtime.CompilerServices;
using Dolittle.Logging;

namespace Dolittle.Build
{
    /// <inheritdoc/>
    public class BuildToolLogger : IBuildToolLogger
    {
        TextWriter _writer = Console.Out;

        
        /// <inheritdoc/>
        public void Critical(string message)
        {
            LogWithCallerInformation(LogLevel.Critical, message);
        }
        
        /// <inheritdoc/>
        public void Debug(string message)
        {
            LogWithCallerInformation(LogLevel.Debug, message);
        }
        /// <inheritdoc/>
        public void Error(string message)
        {
            LogWithCallerInformation(LogLevel.Error, message);
        }
        /// <inheritdoc/>
        public void Error(Exception exception, string message)
        {
            LogWithCallerInformation(LogLevel.Error, message, exception);
        }
        /// <inheritdoc/>
        public void Information(string message)
        {
            LogWithCallerInformation(LogLevel.Info, message);
        }
        /// <inheritdoc/>
        public void Trace(string message)
        {
            LogWithCallerInformation(LogLevel.Trace, message);
        }
        /// <inheritdoc/>
        public void Warning(string message)
        {
            LogWithCallerInformation(LogLevel.Warning, message);
        }

        void ConfigureWriterAndConsole(LogLevel level)
        {
            if (level == LogLevel.Error) _writer = Console.Error;
            else _writer = Console.Out;


        }


        void LogWithCallerInformation(LogLevel level, string message, Exception exception = null)
        {
            ConfigureWriterAndConsole(level);   
            
            var logMessage = $"{message}";
            if (exception != null)
                logMessage += $"\nException: {exception.Message}";

            if (level == LogLevel.Debug || level == LogLevel.Info || level == LogLevel.Trace ) logMessage = logMessage.ToWhiteString();
            else if (level == LogLevel.Error || level == LogLevel.Critical) logMessage = logMessage.ToRedString();
            else logMessage = logMessage.ToYellowString();

            _writer.WriteLine(logMessage);
            
        }
    }
}