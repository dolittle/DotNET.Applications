// Copyright (c) Dolittle. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;

namespace Dolittle.Events.Filters
{
    /// <summary>
    /// Exception that gets thrown when a filter has already been registered with a given <see cref="FilterId" />.
    /// </summary>
    public class FilterAlreadyRegistered : Exception
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="FilterAlreadyRegistered"/> class.
        /// </summary>
        /// <param name="filterId">The <see cref="FilterId" />.</param>
        public FilterAlreadyRegistered(FilterId filterId)
            : base($"A filter with id '{filterId}' has already been registered")
        {
        }
    }
}