// Copyright (c) Dolittle. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System.Threading;
using System.Threading.Tasks;

namespace Dolittle.Events.Handling
{
    /// <summary>
    /// Defines a system that can handle processing for <see cref="EventHandler"/>.
    /// </summary>
    public interface IEventHandlerProcessor
    {
        /// <summary>
        /// Start processing for a specific <see cref="EventHandler"/>.
        /// </summary>
        /// <param name="eventHandler"><see cref="EventHandler"/> to start processing.</param>
        /// <param name="token">Optional. A <see cref="CancellationToken" /> to cancel the operation.</param>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
        Task Start(EventHandler eventHandler, CancellationToken token = default);
    }
}