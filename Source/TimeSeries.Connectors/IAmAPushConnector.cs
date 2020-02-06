// Copyright (c) Dolittle. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System.Threading.Tasks;
using Dolittle.TimeSeries.DataPoints;

namespace Dolittle.TimeSeries.Connectors
{
    /// <summary>
    /// Represents a connector type that connects and pushes data from the source at the cadence decided by the source.
    /// </summary>
    public interface IAmAPushConnector
    {
        /// <summary>
        /// Gets the name of the connector.
        /// </summary>
        Source Name { get; }

        /// <summary>
        /// Connect to the system with the.
        /// </summary>
        /// <param name="writer"><see cref="IStreamWriter"/> used for writing <see cref="TagDataPoint"/>.</param>
        /// <returns><see cref="Task"/> for continuation.</returns>
        Task Connect(IStreamWriter writer);
    }
}