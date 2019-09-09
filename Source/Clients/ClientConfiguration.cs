/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using Dolittle.Configuration;

namespace Dolittle.Clients
{
    /// <summary>
    /// Represents the <see cref="IConfigurationObject">configuration object</see> for <see cref="Client"/>
    /// </summary>
    /// <remarks>
    /// This is representing the information needed to connect to a runtime
    /// </remarks>
    [Name("client")]
    public class ClientConfiguration : IConfigurationObject
    {
        /// <summary>
        /// Initializes a new instance of <see cref="ClientConfiguration"/>
        /// </summary>
        /// <param name="host">Hostname for the runtime</param>
        /// <param name="port"></param>
        public ClientConfiguration(string host, int port)
        {
            Host = host;
            Port = port;
        }

        /// <summary>
        /// Gets the name of the host that holds the runtime instance
        /// </summary>
        public string Host { get; }

        /// <summary>
        /// Gets the port to connect to for the runtime instance
        /// </summary>
        public int Port { get; }
    }
}