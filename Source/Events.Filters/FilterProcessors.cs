// Copyright (c) Dolittle. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Dolittle.Types;

namespace Dolittle.Events.Filters
{
    /// <summary>
    /// Represents an implementation of <see cref="IEventStreamFilter" />.
    /// </summary>
    public class FilterProcessors : IFilterProcessors
    {
        readonly IEnumerable<IFilterProcessor> _filterProcessors;

        /// <summary>
        /// Initializes a new instance of the <see cref="FilterProcessors"/> class.
        /// </summary>
        /// <param name="filterProcessors">The <see cref="IInstancesOf{T}" /> <see cref="IFilterProcessor" />.</param>
        public FilterProcessors(IInstancesOf<IFilterProcessor> filterProcessors)
        {
            _filterProcessors = filterProcessors;
        }

        /// <inheritdoc/>
        public Task Start(IEventStreamFilter filter, CancellationToken token)
        {
            IFilterProcessor processor = null;

            foreach (var filterProcessor in _filterProcessors)
            {
                if (filterProcessor.CanProcess(filter))
                {
                    if (processor != null) throw new MultipleFilterProcessorsForFilter(filter);
                    processor = filterProcessor;
                }
            }

            if (processor == null) throw new NoFilterProcessorForFilter(filter);
            return processor.Start(filter);
        }
    }
}