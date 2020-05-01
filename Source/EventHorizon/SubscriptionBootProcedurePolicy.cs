// Copyright (c) Dolittle. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using Dolittle.Logging;
using Dolittle.Resilience;
using Polly;

namespace Dolittle.EventHorizon
{
    /// <summary>
    /// Defines the policy for registering event horizons subscriptions to the Runtime.
    /// </summary>
    public class SubscriptionBootProcedurePolicy : IDefineAsyncPolicyForType
    {
        readonly ILogger _logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="SubscriptionBootProcedurePolicy"/> class.
        /// </summary>
        /// <param name="logger">The <see cref="ILogger"/> to use for logging.</param>
        public SubscriptionBootProcedurePolicy(ILogger<SubscriptionBootProcedure> logger)
        {
            _logger = logger;
        }

        /// <inheritdoc/>
        public Type Type => typeof(SubscriptionBootProcedure);

        /// <inheritdoc/>
        public Polly.IAsyncPolicy Define()
            => Polly.Policy
                .Handle<Exception>()
                .WaitAndRetryForeverAsync(
                    attempt => TimeSpan.FromSeconds(Math.Min(Math.Pow(2, attempt), 60)),
                    (ex, _) => _logger.Warning(ex, "Error while subscribing to event horizon"));
    }
}