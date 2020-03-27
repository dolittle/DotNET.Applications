// Copyright (c) Dolittle. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using System.Threading.Tasks;
using Dolittle.Events;
using Dolittle.Events.Filters;
using Dolittle.Events.Filters.EventHorizon;
using Dolittle.Logging;

namespace EventSourcing
{
    public class FirstPublicFilter : ICanFilterPublicEvents
    {
        readonly ILogger _logger;

        public FirstPublicFilter(ILogger logger)
        {
            _logger = logger;
        }

        public StreamId SourceStreamId => StreamId.AllStream;

        public FilterId Identifier => Guid.Parse("eb356fbc-59f1-486a-909f-1a20eb1e9ee5");

        public Task<FilterResult> Filter(CommittedEvent @event)
        {
            _logger.Warning($"Filtering public event '{@event}'");
            return Task.FromResult(new FilterResult(true));
        }
    }
}
