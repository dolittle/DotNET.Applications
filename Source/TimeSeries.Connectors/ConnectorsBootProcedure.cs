// Copyright (c) Dolittle. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using Dolittle.Booting;
using Dolittle.Services;

namespace Dolittle.TimeSeries.Connectors
{
    /// <summary>
    /// Represents a <see cref="ICanPerformBootProcedure">boot procedure</see> that hooks up.
    /// </summary>
    public class ConnectorsBootProcedure : ICanPerformBootProcedure
    {
        readonly IPullConnectors _pullConnectors;
        readonly IPushConnectors _pushConnectors;

        /// <summary>
        /// Initializes a new instance of the <see cref="ConnectorsBootProcedure"/> class.
        /// </summary>
        /// <param name="pullConnectors">System for <see cref="IPullConnectors"/>.</param>
        /// <param name="pushConnectors">System for <see cref="IPushConnectors"/>.</param>
        public ConnectorsBootProcedure(IPullConnectors pullConnectors, IPushConnectors pushConnectors)
        {
            _pullConnectors = pullConnectors;
            _pushConnectors = pushConnectors;
        }

        /// <inheritdoc/>
        public bool CanPerform() => EndpointsBootProcedure.EndpointsReady;

        /// <inheritdoc/>
        public void Perform()
        {
            _pullConnectors.Register();
            _pushConnectors.Register();
        }
    }
}