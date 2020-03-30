// Copyright (c) Dolittle. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;

namespace Dolittle.Events.Filters
{
    /// <summary>
    /// Exception that gets thrown when there are no instances of <see cref="IFilterProcessor" /> that can start processing a filter.
    /// </summary>
    public class NoFilterProcessorForFilter : Exception
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="NoFilterProcessorForFilter"/> class.
        /// </summary>
        /// <param name="filter">The <see cref="IEventStreamFilter" />.</param>
        public NoFilterProcessorForFilter(IEventStreamFilter filter)
            : base($"There are no instances of '{typeof(IFilterProcessor).FullName}' that can start processing filter of type '{filter.GetType().FullName}'")
        {
        }
    }
}