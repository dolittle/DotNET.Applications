/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using System;
using System.Net;
using System.Net.Sockets;
using Dolittle.DependencyInversion;

namespace Dolittle.Client
{
    /// <summary>
    /// Provides bindings related to client
    /// </summary>
    public class Bindings : ICanProvideBindings
    {
        /// <inheritdoc/>
        public void Provide(IBindingProviderBuilder builder)
        {
            builder.Bind<ClientId>().To(Guid.NewGuid()).Singleton();

            var listener = new TcpListener(IPAddress.Loopback, 0);
            listener.Start();
            uint port = (uint)((IPEndPoint) listener.LocalEndpoint).Port;
            listener.Stop();
            builder.Bind<ClientPort>().To(port).Singleton();
        }
    }
}