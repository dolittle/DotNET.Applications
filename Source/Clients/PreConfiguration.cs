/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/

using System.Net;
using System.Net.Sockets;
using Dolittle.Booting;
using Dolittle.Services;

namespace Dolittle.Clients
{
    /// <summary>
    /// 
    /// </summary>
    public class PreConfiguration : ICanRunBeforeBootStage<NoSettings>
    {
        internal static ClientPort ClientPort;

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
        }
    }
}