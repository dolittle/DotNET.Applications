// Copyright (c) Dolittle. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;

namespace Dolittle.Events.Filters
{
    /// <summary>
    /// Exception that gets thrown when a filter with a specific <see cref="FilterId"/> is not registered in the system.
    /// </summary>
    public class MissingFilterWithId : Exception
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="MissingFilterWithId"/> class.
        /// </summary>
        /// <param name="filterId">The <see cref="FilterId">identifier</see>.</param>
        public MissingFilterWithId(FilterId filterId)
            : base($"Filter with {filterId} is not registered")
        {
        }
    }
}