// Copyright (c) Dolittle. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using Dolittle.Concepts;

namespace Dolittle.Events.Processing
{
    /// <summary>
    /// Represents the concept of a unique identifier for a stream.
    /// </summary>
    public class StreamId : ConceptAs<Guid>
    {
        /// <summary>
        /// Represents the all stream <see cref="StreamId"/>.
        /// </summary>
        public static StreamId AllStream = Guid.Empty;

        /// <summary>
        /// Implicitly convert from <see cref="Guid"/> to <see cref="StreamId"/>.
        /// </summary>
        /// <param name="id"><see cref="Guid"/> to convert from.</param>
        public static implicit operator StreamId(Guid id) => new StreamId { Value = id };

        /// <summary>
        /// Creates a new instance of <see cref="StreamId"/> with a unique id.
        /// </summary>
        /// <returns>A new <see cref="StreamId"/>.</returns>
        public static StreamId New()
        {
            return new StreamId { Value = Guid.NewGuid() };
        }
    }
}