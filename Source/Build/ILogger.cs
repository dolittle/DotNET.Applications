using System;
using System.IO;
using System.Runtime.CompilerServices;

namespace Dolittle.Build
{
    /// <summary>
    /// Defines a logger that's used in the Build Tool
    /// </summary>
    public interface IBuildToolLogger
    {

        /// <summary>
        /// Logs a critical message
        /// </summary>
        /// <param name="message"></param>
        void Critical(string message);

        /// <summary>
        /// Logs a debug message
        /// </summary>
        /// <param name="message"></param>
        void Debug(string message);

        /// <summary>
        /// Logs an error message
        /// </summary>
        /// <param name="message"></param>
        void Error(string message);
        /// <summary>
        /// Logs an error message
        /// </summary>
        /// <param name="exception"></param>
        /// <param name="message"></param>
        void Error(Exception exception, string message);
        
        /// <summary>
        /// Logs an information message
        /// </summary>
        /// <param name="message"></param>
        void Information(string message);

        /// <summary>
        /// Logs a trace message
        /// </summary>
        /// <param name="message"></param>
        void Trace(string message);

        /// <summary>
        /// Logs a warning message
        /// </summary>
        /// <param name="message"></param>
        void Warning(string message);
    }
}