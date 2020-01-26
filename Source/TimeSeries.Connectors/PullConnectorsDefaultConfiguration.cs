// Copyright (c) Dolittle. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System.Collections.Generic;
using Dolittle.Configuration;
using Dolittle.TimeSeries.DataPoints;

namespace Dolittle.TimeSeries.Connectors
{
    /// <summary>
    /// Provides default configuration for <see cref="PullConnectorsConfiguration"/>.
    /// </summary>
    public class PullConnectorsDefaultConfiguration : ICanProvideDefaultConfigurationFor<PullConnectorsConfiguration>
    {
        /// <inheritdoc/>
        public PullConnectorsConfiguration Provide()
        {
            return new PullConnectorsConfiguration(new Dictionary<Source, PullConnectorConfiguration>());
        }
    }
}