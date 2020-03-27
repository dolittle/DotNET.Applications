// Copyright (c) Dolittle. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;

namespace Dolittle.Events.Filters
{
    /// <summary>
    /// Exception that gets thrown when there are multiple instances of <see cref="IEventStreamFilter" /> that can start processing a filter.
    /// </summary>
    public class MultipleFilterProcessorsForFilter : Exception
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="MultipleFilterProcessorsForFilter"/> class.
        /// </summary>
        /// <param name="filter">The <see cref="IEventStreamFilter" />.</param>
        public MultipleFilterProcessorsForFilter(IEventStreamFilter filter)
            : base($"There are multiple instances of '{typeof(IFilterProcessor).FullName}' that can start processing filter of type '{filter.GetType().FullName}'")
        {
        }
    }
}