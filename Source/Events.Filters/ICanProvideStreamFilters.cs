// Copyright (c) Dolittle. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System.Collections.Generic;

namespace Dolittle.Events.Filters
{
    /// <summary>
    /// Represents a system that is capable of providing <see cref="IEventStreamFilter"/>.
    /// </summary>
    public interface ICanProvideStreamFilters
    {
        /// <summary>
        /// Provide <see cref="IEnumerable{T}"/> of <see cref="IEventStreamFilter"/>.
        /// </summary>
        /// <returns><see cref="IEnumerable{T}"/> of <see cref="IEventStreamFilter"/>.</returns>
        IEnumerable<IEventStreamFilter> Provide();
    }
}