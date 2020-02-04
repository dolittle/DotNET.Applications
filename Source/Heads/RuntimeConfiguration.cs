// Copyright (c) Dolittle. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using Dolittle.Configuration;

namespace Dolittle.Heads
{
    /// <summary>
    /// Represents the <see cref="IConfigurationObject">configuration object</see> for <see cref="Head"/>.
    /// </summary>
    /// <remarks>
    /// This is representing the information needed to connect to a runtime.
    /// </remarks>
    [Name("runtime")]
    public class RuntimeConfiguration : IConfigurationObject
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="RuntimeConfiguration"/> class.
        /// </summary>
        /// <param name="host">Hostname for the runtime.</param>
        /// <param name="port">Port to connect to for the runtime.</param>
        public RuntimeConfiguration(string host, int port)
        {
            Host = host;
            Port = port;
        }

        /// <summary>
        /// Gets the name of the host that holds the runtime instance.
        /// </summary>
        public string Host { get; }

        /// <summary>
        /// Gets the port to connect to for the runtime instance.
        /// </summary>
        public int Port { get; }
    }
}