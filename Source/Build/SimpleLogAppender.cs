/*---------------------------------------------------------------------------------------------
*  Copyright (c) Dolittle. All rights reserved.
*  Licensed under the MIT License. See LICENSE in the project root for license information.
*--------------------------------------------------------------------------------------------*/
using System;
using System.Collections.Generic;
using System.IO;
using System.Diagnostics;
using Dolittle.Logging;

namespace Dolittle.Build
{
    /// <summary>
    /// Represents a simple implementation of <see cref="ILogAppender"/> for using System.Diagnostics.Debug
    /// </summary>
    public class SimpleLogAppender : ILogAppender
    {
        /// <inheritdoc/>
        public void Append(string filePath, int lineNumber, string member, LogLevel level, string message, Exception exception = null)
        {
            if (level == LogLevel.Trace || level == LogLevel.Info) 
            {
                Console.BackgroundColor = ConsoleColor.White;
            }
            else if (level == LogLevel.Warning || level == LogLevel.Debug)
            {
                Console.BackgroundColor = ConsoleColor.Yellow;
            }
            else if(level == LogLevel.Critical || level == LogLevel.Error)
            {
                Console.BackgroundColor = ConsoleColor.Red;
            }

            Console.WriteLine($"{message}");

        }
    }
}
