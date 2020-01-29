// Copyright (c) Dolittle. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System.Collections.Generic;
using System.Collections.ObjectModel;
using Dolittle.Configuration;
using Dolittle.TimeSeries.DataPoints;

namespace Dolittle.TimeSeries.Connectors
{
    /// <summary>
    /// Represents the configuration for <see cref="IPullConnectors"/>.
    /// </summary>
    [Name("pullconnectors")]
    public class PullConnectorsConfiguration :
        ReadOnlyDictionary<Source, PullConnectorConfiguration>,
        IConfigurationObject
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="PullConnectorsConfiguration"/> class.
        /// </summary>
        /// <param name="sources">Configuration instance - passed along to be made immutable.</param>
        public PullConnectorsConfiguration(IDictionary<Source, PullConnectorConfiguration> sources)
            : base(sources)
        {
        }
    }
}