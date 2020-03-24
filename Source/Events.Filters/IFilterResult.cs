// Copyright (c) Dolittle. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using Dolittle.Events.Processing;

namespace Dolittle.Events.Filters
{
    /// <summary>
    /// Represents the result of a filtering.
    /// </summary>
    public interface IFilterResult : IProcessingResult
    {
        /// <summary>
        /// Gets a value indicating whether or not it should be included.
        /// </summary>
        bool IsIncluded { get; }

        /// <summary>
        /// Gets the <see cref="PartitionId"/> it belongs to.
        /// </summary>
        PartitionId Partition { get; }
    }
}