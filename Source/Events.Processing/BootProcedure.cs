// Copyright (c) Dolittle. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using Dolittle.Booting;

namespace Dolittle.Events.Processing
{
    /// <summary>
    /// Represents a <see cref="ICanPerformBootProcedure"/> for settings up all filters and processors.
    /// </summary>
    public class BootProcedure : ICanPerformBootProcedure
    {
        readonly IStreamFilters _filters;

        /// <summary>
        /// Initializes a new instance of the <see cref="BootProcedure"/> class.
        /// </summary>
        /// <param name="filters">All the <see cref="IStreamFilters"/>.</param>
        public BootProcedure(IStreamFilters filters)
        {
            _filters = filters;
        }

        /// <inheritdoc/>
        public bool CanPerform() => Artifacts.Configuration.BootProcedure.HasPerformed;

        /// <inheritdoc/>
        public void Perform()
        {
            _filters.Register();
        }
    }
}