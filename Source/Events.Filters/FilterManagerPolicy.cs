// Copyright (c) Dolittle. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using Dolittle.Logging;
using Dolittle.Resilience;
using Polly;

namespace Dolittle.Events.Filters
{
    /// <summary>
    /// Defines the policy for registering event filters to the Runtime.
    /// </summary>
    public class FilterManagerPolicy : IDefineAsyncPolicyForType
    {
        readonly ILogger<FilterManager> _logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="FilterManagerPolicy"/> class.
        /// </summary>
        /// <param name="logger">The <see cref="ILogger"/> to use for logging.</param>
        public FilterManagerPolicy(ILogger<FilterManager> logger)
        {
            _logger = logger;
        }

        /// <inheritdoc/>
        public Type Type => typeof(FilterManager);

        /// <inheritdoc/>
        public Polly.IAsyncPolicy Define()
            => Polly.Policy
                .Handle<Exception>()
                .WaitAndRetryForeverAsync(
                    attempt => TimeSpan.FromSeconds(Math.Min(Math.Pow(2, attempt), 60)),
                    (ex, _) => _logger.Warning(ex, "Error while registering filter"));
    }
}