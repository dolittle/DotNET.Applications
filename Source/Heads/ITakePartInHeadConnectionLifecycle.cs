// Copyright (c) Dolittle. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

extern alias contracts;

using System.Threading;
using System.Threading.Tasks;

namespace Dolittle.Heads
{
    /// <summary>
    /// Defines a system for handling procedures for heads and their lifecycle.
    /// </summary>
    public interface ITakePartInHeadConnectionLifecycle
    {
        /// <summary>
        /// Called during booting to check if we're ready to start the lifecycle head system.
        /// </summary>
        /// <returns>true if ready, false if not.</returns>
        bool IsReady();

        /// <summary>
        /// Called when the head is connected.
        /// </summary>
        /// <param name="token">Optional. A <see cref="CancellationToken" /> to cancel the operation.</param>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
        Task OnConnected(CancellationToken token = default);
    }
}