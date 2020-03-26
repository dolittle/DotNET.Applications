// Copyright (c) Dolittle. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using Dolittle.Logging;
using Dolittle.Resilience;
using Polly;

namespace Dolittle.Events.Processing
{
    /// <summary>
    /// Defines the policy for writing event processing response to runtime.
    /// </summary>
    public class WriteEventProcessingResponsePolicy : IDefineNamedAsyncPolicy
    {
        readonly ILogger _logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="WriteEventProcessingResponsePolicy"/> class.
        /// </summary>
        /// <param name="logger"><see cref="ILogger"/> for logging.</param>
        public WriteEventProcessingResponsePolicy(ILogger logger)
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
                    _logger.Warning($"Failed to write event processing response to runtime : {_.Message}");
                    return true;
                })
                .WaitAndRetryForeverAsync(_ => TimeSpan.FromSeconds(1));
    }
}