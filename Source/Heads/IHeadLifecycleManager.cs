// Copyright (c) Dolittle. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

extern alias contracts;

namespace Dolittle.Heads
{
    /// <summary>
    /// Defines a system for managing the lifecycle of the head.
    /// </summary>
    public interface IHeadLifecycleManager
    {
        /// <summary>
        /// Check whether or not the manager is ready to start.
        /// </summary>
        /// <returns>true if ready, false if not.</returns>
        bool IsReady();

        /// <summary>
        /// Start managing the head and its lifecycle.
        /// </summary>
        void Start();
    }
}