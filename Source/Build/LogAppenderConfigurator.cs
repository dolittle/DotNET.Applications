/*---------------------------------------------------------------------------------------------
*  Copyright (c) Dolittle. All rights reserved.
*  Licensed under the MIT License. See LICENSE in the project root for license information.
*--------------------------------------------------------------------------------------------*/
using Dolittle.Logging;

namespace Dolittle.Build
{
    /// <inheritdoc/>
    public class LogAppenderConfigurator : ICanConfigureLogAppenders
    {
        /// <inheritdoc/>
        public void Configure(ILogAppenders logAppenders)
        {
            logAppenders.Clear();
            logAppenders.Add(new SimpleLogAppender());
        }
    }
}