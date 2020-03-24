// Copyright (c) Dolittle. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;

namespace Dolittle.Events.Filters
{
    /// <summary>
    /// Exception that gets thrown when <see cref="FilterId" /> is an illegal value.
    /// </summary>
    public class IllegalFilterId : Exception
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="IllegalFilterId"/> class.
        /// </summary>
        /// <param name="filterId">The <see cref="FilterId" />.</param>
        public IllegalFilterId(FilterId filterId)
            : base($"Filter id cannot be '{filterId}'")
        {
        }
    }
}