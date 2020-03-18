// Copyright (c) Dolittle. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using Dolittle.Booting;

namespace Dolittle.Heads
{
    /// <summary>
    /// Performs boot procedures related to client.
    /// </summary>
    public class BootProcedure : ICanPerformBootProcedure
    {
        readonly IHeadLifecycleManager _headLifecycleManager;

        /// <summary>
        /// Initializes a new instance of the <see cref="BootProcedure"/> class.
        /// </summary>
        /// <param name="headLifecycleManager">The <see cref="IHeadLifecycleManager"/>.</param>
        public BootProcedure(IHeadLifecycleManager headLifecycleManager)
        {
            _headLifecycleManager = headLifecycleManager;
        }

        /// <inheritdoc/>
        public bool CanPerform() => _headLifecycleManager.IsReady();

        /// <inheritdoc/>
        public void Perform()
        {
            _headLifecycleManager.Start();
        }
    }
}