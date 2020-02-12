// Copyright (c) Dolittle. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace Dolittle.Events.Processing
{
    /// <summary>
    /// Defines a system for handling all <see cref="ICanFilterEventsInStream"/>.
    /// </summary>
    public interface IStreamFilters
    {
        /// <summary>
        /// Register all the <see cref="ICanFilterEventsInStream">stream filters</see>.
        /// </summary>
        void Register();
    }
}