/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using Dolittle.Configuration;

namespace Dolittle.Clients
{
    /// <summary>
    /// Represents the default configuration for <see cref="ClientConfiguration"/> if none is provided
    /// </summary>
    public class ClientConfigurationDefaultProvider : ICanProvideDefaultConfigurationFor<ClientConfiguration>
    {
        /// <inheritdoc/>
        public ClientConfiguration Provide()
        {
            return new ClientConfiguration("0.0.0.0", 50053);
        }
    }
}