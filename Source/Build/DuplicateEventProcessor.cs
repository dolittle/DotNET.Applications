// Copyright (c) Dolittle. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;

namespace Dolittle.Build
{
    /// <summary>
    /// Exception that gets thrown when there are duplicate event processors.
    /// </summary>
    public class DuplicateEventProcessor : Exception
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="DuplicateEventProcessor"/> class.
        /// </summary>
        public DuplicateEventProcessor()
            : base("Found one or more duplications of Event Processors")
        {
        }
    }
}