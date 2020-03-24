// Copyright (c) Dolittle. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System.Linq;
using Dolittle.Booting;
using Dolittle.Collections;
using Dolittle.Types;

namespace Dolittle.Events.Filters
{
    /// <summary>
    /// Represents a <see cref="ICanPerformBootProcedure"/> for settings up event handlers.
    /// </summary>
    public class BootProcedure : ICanPerformBootProcedure
    {
        readonly IInstancesOf<ICanProvideStreamFilters> _streamFilterProviders;
        readonly IStreamFilters _streamFilters;

        /// <summary>
        /// Initializes a new instance of the <see cref="BootProcedure"/> class.
        /// </summary>
        /// <param name="streamFilterProviders"><see cref="IInstancesOf{T}"/> <see cref="ICanProvideStreamFilters"/>.</param>
        /// <param name="streamFilters">The <see cref="IStreamFilters"/> system.</param>
        public BootProcedure(
            IInstancesOf<ICanProvideStreamFilters> streamFilterProviders,
            IStreamFilters streamFilters)
        {
            _streamFilterProviders = streamFilterProviders;
            _streamFilters = streamFilters;
        }

        /// <inheritdoc/>
        public bool CanPerform() => Artifacts.Configuration.BootProcedure.HasPerformed;

        /// <inheritdoc/>
        public void Perform()
        {
            _streamFilterProviders.SelectMany(provider => provider.Provide()).ForEach(filter => _streamFilters.Register(filter));
            _streamFilters.StartProcessingFilters();
        }
    }
}