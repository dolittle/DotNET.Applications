// Copyright (c) Dolittle. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;

namespace Dolittle.Events.Filters
{
    /// <summary>
    /// Exception that gets thrown when an <see cref="IEventStreamFilter">filter</see> does not have the <see cref="FilterAttribute"/>.
    /// </summary>
    public class MissingFilterAttributeForFilter : MissingAttributeForFilter
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="MissingFilterAttributeForFilter"/> class.
        /// </summary>
        /// <param name="filterType">Type of <see cref="IEventStreamFilter"/>.</param>
        public MissingFilterAttributeForFilter(Type filterType)
            : base(filterType, typeof(FilterAttribute), $"\"{Guid.Empty}\"")
        {
        }
    }
}