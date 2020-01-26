// Copyright (c) Dolittle. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace Dolittle.TimeSeries.Connectors
{
    /// <summary>
    /// Represents the configuration for a single pull connector.
    /// </summary>
    public class PullConnectorConfiguration
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="PullConnectorConfiguration"/> class.
        /// </summary>
        /// <param name="interval">Interval in milliseconds for the pulling.</param>
        public PullConnectorConfiguration(int interval)
        {
            Interval = interval;
        }

        /// <summary>
        /// Gets the pull interval in milliseconds used for the connector.
        /// </summary>
        public int Interval { get; }
    }
}