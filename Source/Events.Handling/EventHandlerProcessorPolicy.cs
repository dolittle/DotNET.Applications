// Copyright (c) Dolittle. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using Dolittle.Logging;
using Dolittle.Resilience;
using Polly;

namespace Dolittle.Events.Handling
{
    /// <summary>
    /// Defines the policy for <see cref="EventHandlerProcessorPolicy"/>.
    /// </summary>
    public class EventHandlerProcessorPolicy : IDefineAsyncPolicyForType
    {
        readonly ILogger _logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="EventHandlerProcessorPolicy"/> class.
        /// </summary>
        /// <param name="logger"><see cref="ILogger"/> for logging.</param>
        public EventHandlerProcessorPolicy(ILogger logger)
        {
            _logger = logger;
        }

        /// <inheritdoc/>
        public Type Type => typeof(EventHandlerProcessor);

        /// <inheritdoc/>
        public Polly.IAsyncPolicy Define() =>
            Polly.Policy.Handle<Exception>(_ =>
                {
                    _logger.Warning($"Failed to write event processing result to runtime : {_.Message}");
                    return true;
                })
                .WaitAndRetryForeverAsync(_ => TimeSpan.FromSeconds(1));
    }
}