// Copyright (c) Dolittle. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using Dolittle.Logging;
using Dolittle.Resilience;
using Grpc.Core;
using Polly;

namespace Dolittle.Events.Handling
{
    /// <summary>
    /// Defines the policy for registering event handlers to the Runtime.
    /// </summary>
    public class EventHandlerManagerPolicy : IDefineAsyncPolicyForType
    {
        readonly ILogger<EventHandlerManager> _logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="EventHandlerManagerPolicy"/> class.
        /// </summary>
        /// <param name="logger">The <see cref="ILogger"/> to use for logging.</param>
        public EventHandlerManagerPolicy(ILogger<EventHandlerManager> logger)
        {
            _logger = logger;
        }

        /// <inheritdoc/>
        public Type Type => typeof(EventHandlerManager);

        /// <inheritdoc/>
        public Polly.IAsyncPolicy Define()
            => Polly.Policy
                .Handle<Exception>(
                    _ =>
                    {
                        switch (_)
                        {
                            case RpcException ex:
                            switch (ex.StatusCode)
                            {
                                case StatusCode.Unavailable:
                                return true;
                            }

                            goto default;

                            default:
                            _logger.Warning(_, "Error while registering event handler");
                            return true;
                        }
                    })
                .WaitAndRetryForeverAsync(attempt => TimeSpan.FromSeconds(Math.Min(Math.Pow(2, attempt), 60)));
    }
}
