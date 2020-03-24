// Copyright (c) Dolittle. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System.Collections.Concurrent;
using Dolittle.Collections;
using Dolittle.Logging;

namespace Dolittle.Events.Filters
{
    /// <summary>
    /// Represents an implementation of <see cref="IStreamFilters"/> for registering <see cref="ICanFilterPrivateEvents">filters</see>.
    /// </summary>
    public class StreamFilters : IStreamFilters
    {
        readonly IFilterProcessors _filterProcessors;
        readonly ILogger _logger;
        readonly ConcurrentDictionary<FilterId, IEventStreamFilter> _filters = new ConcurrentDictionary<FilterId, IEventStreamFilter>();
        bool _alreadyStartedProcessing;

        /// <summary>
        /// Initializes a new instance of the <see cref="StreamFilters"/> class.
        /// </summary>
        /// <param name="filterProcessors">The <see cref="IFilterProcessors" />.</param>
        /// <param name="logger">The <see cref="ILogger" />.</param>
        public StreamFilters(IFilterProcessors filterProcessors, ILogger logger)
        {
            _filterProcessors = filterProcessors;
            _logger = logger;
        }

        /// <inheritdoc/>
        public bool HasFor(FilterId filterId) => _filters.ContainsKey(filterId);

        /// <inheritdoc/>
        public IEventStreamFilter GetFor(FilterId filterId)
        {
            ThrowIfMissingFilterWithId(filterId);
            return _filters[filterId];
        }

        /// <inheritdoc/>
        public void Register(IEventStreamFilter filter)
        {
            ThrowIfIllegalFilterId(filter.Identifier);
            if (!_filters.TryAdd(filter.Identifier, filter)) throw new FilterAlreadyRegistered(filter.Identifier);
        }

        /// <inheritdoc/>
        public void StartProcessingFilters()
        {
            if (!_alreadyStartedProcessing)
            {
                _alreadyStartedProcessing = true;
                _filters.ForEach(_ => _filterProcessors.Start(_.Value));
            }
        }

        void ThrowIfMissingFilterWithId(FilterId filterId)
        {
            if (!HasFor(filterId)) throw new MissingFilterWithId(filterId);
        }

        void ThrowIfIllegalFilterId(FilterId filterId)
        {
            var stream = new StreamId { Value = filterId };
            if (stream.IsNonWriteable) throw new IllegalFilterId(filterId);
        }
    }
}