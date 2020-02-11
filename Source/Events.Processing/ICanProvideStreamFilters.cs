// Copyright (c) Dolittle. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System.Collections.Generic;

namespace Dolittle.Events.Processing
{
    /// <summary>
    /// Represents a system that is capable of providing <see cref="ICanFilterEventsInStream"/>.
    /// </summary>
    public interface ICanProvideStreamFilters
    {
        /// <summary>
        /// Provide <see cref="IEnumerable{T}"/> of <see cref="ICanFilterEventsInStream"/>.
        /// </summary>
        /// <returns><see cref="IEnumerable{T}"/> of <see cref="ICanFilterEventsInStream"/>.</returns>
        IEnumerable<ICanFilterEventsInStream> Provide();
    }
}