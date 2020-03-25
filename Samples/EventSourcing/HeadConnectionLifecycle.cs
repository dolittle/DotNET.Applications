// Copyright (c) Dolittle. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System.Threading.Tasks;
using Dolittle.Heads;

namespace EventSourcing
{
    public class HeadConnectionLifecycle : ITakePartInHeadConnectionLifecycle
    {
        static readonly TaskCompletionSource<bool> _tcs = new TaskCompletionSource<bool>();

        /// <summary>
        /// Gets the task that can be awaited for when we're connected.
        /// </summary>
        public static Task Connected => _tcs.Task;

        /// <inheritdoc/>
        public bool IsReady() => true;

        /// <inheritdoc/>
        public void OnConnected()
        {
            _tcs.TrySetResult(true);
        }

        /// <inheritdoc/>
        public void OnDisconnected()
        {
            _tcs.TrySetResult(false);
        }
    }
}
