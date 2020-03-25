// Copyright (c) Dolittle. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System.Threading;
using System.Threading.Tasks;
using Dolittle.Heads;

namespace Dolittle.Events.Processing
{
    /// <summary>
    /// Represents a <see cref="ITakePartInHeadConnectionLifecycle"/> for settings up all filters and processors.
    /// </summary>
    public class HeadConnectionLifecycle : ITakePartInHeadConnectionLifecycle
    {
        readonly IStreamFilters _filters;

        /// <summary>
        /// Initializes a new instance of the <see cref="HeadConnectionLifecycle"/> class.
        /// </summary>
        /// <param name="filters">All the <see cref="IStreamFilters"/>.</param>
        public HeadConnectionLifecycle(IStreamFilters filters)
        {
            _filters = filters;
        }

        /// <inheritdoc/>
        public bool IsReady() => Artifacts.Configuration.BootProcedure.HasPerformed;

        /// <inheritdoc/>
        public Task OnConnected(CancellationToken token)
        {
            _filters.Register();
            return new TaskCompletionSource<bool>().Task;
        }
    }
}