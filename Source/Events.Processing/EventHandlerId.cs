// Copyright (c) Dolittle. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using Dolittle.Concepts;

namespace Dolittle.Events.Processing
{
    /// <summary>
    /// Represents the concept of an event handler.
    /// </summary>
    public class EventHandlerId : ConceptAs<Guid>
    {
        /// <summary>
        /// Implicitly convert from <see cref="Guid"/> to <see cref="EventHandlerId"/>.
        /// </summary>
        /// <param name="id"><see cref="Guid"/> to convert from.</param>
        /// <returns><see cref="EventHandlerId"/> instance.</returns>
        public static implicit operator EventHandlerId(Guid id) => new EventHandlerId { Value = id };
    }
}