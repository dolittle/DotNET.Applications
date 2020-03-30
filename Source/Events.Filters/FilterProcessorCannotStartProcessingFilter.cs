// Copyright (c) Dolittle. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;

namespace Dolittle.Events.Filters
{
    /// <summary>
    /// Exception that gets thrown when an <see cref="IFilterProcessor" /> cannot start processing a filter.
    /// </summary>
    public class FilterProcessorCannotStartProcessingFilter : Exception
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="FilterProcessorCannotStartProcessingFilter"/> class.
        /// </summary>
        /// <param name="filterProcessor">The <see cref="IFilterProcessor" />.</param>
        /// <param name="filter">The <see cref="IEventStreamFilter" />.</param>
        public FilterProcessorCannotStartProcessingFilter(IFilterProcessor filterProcessor, IEventStreamFilter filter)
            : base($"The filter processor '{filterProcessor.GetType().FullName}' cannot start processing filter of type '{filter.GetType().FullName}'")
        {
        }
    }
}