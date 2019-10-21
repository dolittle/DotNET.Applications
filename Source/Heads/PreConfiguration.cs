/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/

using System.Net;
using System.Net.Sockets;
using Dolittle.Booting;
using Dolittle.Services;

namespace Dolittle.Heads
{
    /// <summary>
    /// Represents a system that runs before the <see cref="BootStage.Configuration"/> boot stage
    /// </summary>
    public class PreConfiguration : ICanRunBeforeBootStage<NoSettings>
    {
        internal static HeadPort ClientPort;

        /// <inheritdoc/>
        public BootStage BootStage => BootStage.Configuration;

        /// <inheritdoc/>
        public void Perform(NoSettings settings, IBootStageBuilder builder)
        {
            var listener = new TcpListener(IPAddress.Loopback, 0);
            listener.Start();
            ClientPort = ((IPEndPoint) listener.LocalEndpoint).Port;
            listener.Stop();

            EndpointConfigurationDefaultProvider.DefaultPrivatePort = ClientPort;

            listener = new TcpListener(IPAddress.Loopback, 0);
            listener.Start();
            var publicPort = ((IPEndPoint) listener.LocalEndpoint).Port;
            listener.Stop();

            EndpointConfigurationDefaultProvider.DefaultPublicPort = publicPort;
        }
    }
}