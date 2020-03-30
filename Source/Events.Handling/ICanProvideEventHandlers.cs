// Copyright (c) Dolittle. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System.Collections.Generic;

namespace Dolittle.Events.Handling
{
    /// <summary>
    /// Defines a system that can provide <see cref="AbstractEventHandler" /> implementations.
    /// </summary>
    public interface ICanProvideEventHandlers
    {
        /// <summary>
        /// Provides instances of <see cref="AbstractEventHandler"/>.
        /// </summary>
        /// <returns><see cref="IEnumerable{T}"/> of <see cref="AbstractEventHandler" />.</returns>
        IEnumerable<AbstractEventHandler> Provide();
    }
}