// Copyright (c) Dolittle. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System.Threading;
using System.Threading.Tasks;

namespace Dolittle.Events.Filters
{
    /// <summary>
    /// Defines a system that knows about <see cref="IFilterProcessor" />.
    /// </summary>
    public interface IFilterProcessors
    {
        /// <summary>
        /// Start processing for a specific <see cref="IEventStreamFilter"> filter</see>.
        /// </summary>
        /// <param name="filter"><see cref="IEventStreamFilter"/> to start processing.</param>
        /// <param name="token">Optional. A <see cref="CancellationToken" /> to cancel the operation.</param>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
        Task Start(IEventStreamFilter filter, CancellationToken token = default);
    }
}