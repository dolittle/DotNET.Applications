// Copyright (c) Dolittle. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace Dolittle.TimeSeries.Connectors
{
    /// <summary>
    /// Defines a system for working with <see cref="IAmAPullConnector"/>.
    /// </summary>
    public interface IPullConnectors
    {
        /// <summary>
        /// Register all pull connectors.
        /// </summary>
        void Register();

        /// <summary>
        /// Get a <see cref="IAmAPullConnector"/> by its <see cref="ConnectorId"/>.
        /// </summary>
        /// <param name="connectorId"><see cref="ConnectorId"/> representing the connector.</param>
        /// <returns>Instance of <see cref="IAmAPullConnector"/>.</returns>
        IAmAPullConnector GetById(ConnectorId connectorId);
    }
}