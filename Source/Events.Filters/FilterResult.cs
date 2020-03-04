// Copyright (c) Dolittle. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace Dolittle.Events.Filters
{
    /// <summary>
    /// Represents the result of a <see cref="ICanFilterEventsInStream"/>.
    /// </summary>
    public class FilterResult
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="FilterResult"/> class.
        /// </summary>
        /// <param name="isIncluded">Boolean indicating whether or not it should be included.</param>
        /// <remarks>
        /// Using this overload indicates that the partition is considered unspecified.
        /// This means it will filter without considering a partition strategy.
        /// </remarks>
        public FilterResult(bool isIncluded)
        {
            IsIncluded = isIncluded;
            Partition = PartitionId.Unspecificied;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="FilterResult"/> class.
        /// </summary>
        /// <param name="isIncluded">Boolean indicating whether or not it should be included.</param>
        /// <param name="partition"><see cref="PartitionId"/> it belongs to.</param>
        public FilterResult(bool isIncluded, PartitionId partition)
        {
            IsIncluded = isIncluded;
            Partition = partition;
        }

        /// <summary>
        /// Gets a value indicating whether or not it should be included.
        /// </summary>
        public bool IsIncluded { get; }

        /// <summary>
        /// Gets the <see cref="PartitionId"/> it belongs to.
        /// </summary>
        public PartitionId Partition { get; }

        /// <summary>
        /// Implicitly convert from a bool to <see cref="FilterResult"/>.
        /// </summary>
        /// <param name="isIncluded">true if it should be included, false if not.</param>
        public static implicit operator FilterResult(bool isIncluded) => new FilterResult(isIncluded);
    }
}