// Copyright (c) Dolittle. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using System.Threading.Tasks;
using Dolittle.Events;
using Dolittle.Events.Filters;
using Dolittle.Events.Filters.EventHorizon;
using Dolittle.Logging;

namespace Head
{
    [Filter("82f35eaa-8317-4c8b-9bd6-f16c212fda96")]
    public class PublicFilter : ICanFilterPublicEvents
    {
        readonly ILogger _logger;

        public PublicFilter(ILogger<PublicFilter> logger)
        {
            _logger = logger;
        }

        public Task<PublicFilterResult> Filter(IPublicEvent @event, EventContext eventContext)
        {
            _logger.Information($"Filtering event");
            return Task.FromResult(new PublicFilterResult(true, Guid.Empty));
        }
    }
}