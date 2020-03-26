// Copyright (c) Dolittle. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using Dolittle.Logging;
using Dolittle.Resilience;
using Polly;

namespace Dolittle.Events.Processing
{
    /// <summary>
    /// Defines the policy for starting an event processor.
    /// </summary>
    public class StartEventProcessorPolicy : IDefineNamedAsyncPolicy
    {
        readonly ILogger _logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="StartEventProcessorPolicy"/> class.
        /// </summary>
        /// <param name="logger"><see cref="ILogger"/> for logging.</param>
        public StartEventProcessorPolicy(ILogger logger)
        {
            _logger = logger;
            Name = GetType().Name;
        }

        /// <inheritdoc/>
        public string Name { get; }

        /// <inheritdoc/>
        public Polly.IAsyncPolicy Define() =>
            Polly.Policy.Handle<Exception>(_ =>
                {
                    _logger.Warning($"Failed to start event processor : {_.Message}");
                    return true;
                })
                .WaitAndRetryForeverAsync(_ => TimeSpan.FromSeconds(5));
    }
}