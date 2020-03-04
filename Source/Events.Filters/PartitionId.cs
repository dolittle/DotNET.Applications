// Copyright (c) Dolittle. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using Dolittle.Concepts;

namespace Dolittle.Events.Filters
{
    /// <summary>
    /// Represents the concept of a unique identifier for a partition.
    /// </summary>
    public class PartitionId : ConceptAs<Guid>
    {
        /// <summary>
        /// The identifier for when a partition is not specified.
        /// </summary>
        public static PartitionId Unspecificied = Guid.Empty;

        /// <summary>
        /// Implicitly convert from <see cref="Guid"/> to <see cref="PartitionId"/>.
        /// </summary>
        /// <param name="id"><see cref="Guid"/> to convert from.</param>
        public static implicit operator PartitionId(Guid id) => new PartitionId { Value = id };
    }
}