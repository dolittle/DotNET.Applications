// Copyright (c) Dolittle. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System.Collections.Generic;
using System.Linq;
using Dolittle.Logging;
using Dolittle.Types;

namespace Dolittle.Events.Filters.EventHorizon
{
    /// <summary>
    /// Represents an implementation of <see cref="ICanProvideStreamFilters" /> that provides filters that filter external events.
    /// </summary>
    public class ExternalEventsFilterProvider : ICanProvideStreamFilters
    {
        readonly IEnumerable<ICanFilterExternalEvents> _filters;
        readonly ILogger _logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="ExternalEventsFilterProvider"/> class.
        /// </summary>
        /// <param name="filters">The <see cref="IInstancesOf{T}" /> for <see cref="ICanFilterExternalEvents" />.</param>
        /// <param name="logger">The <see cref="ILogger" />.</param>
        public ExternalEventsFilterProvider(IInstancesOf<ICanFilterExternalEvents> filters, ILogger logger)
        {
            _filters = filters;
            _logger = logger;
        }

        /// <inheritdoc/>
        public IEnumerable<IEventStreamFilter> Provide()
        {
            _logger.Debug($"Providing {_filters.Count()} filters that can filter external events.");
            return _filters;
        }
    }
}