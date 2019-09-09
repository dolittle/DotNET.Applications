/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using System;
using Dolittle.DependencyInversion;
using Grpc.Core;

namespace Dolittle.Clients
{
    /// <summary>
    /// Provides bindings related to client
    /// </summary>
    public class Bindings : ICanProvideBindings
    {
        /// <inheritdoc/>
        public void Provide(IBindingProviderBuilder builder)
        {
           var keepAliveTime = new ChannelOption("grpc.keepalive_time", 1000);
            var keepAliveTimeout = new ChannelOption("grpc.keepalive_timeout_ms", 500);
            var keepAliveWithoutCalls = new ChannelOption("grpc.keepalive_permit_without_calls", 1);

            var channel = new Channel("0.0.0.0", 50053, ChannelCredentials.Insecure, new []
            {
                keepAliveTime,
                keepAliveTimeout,
                keepAliveWithoutCalls
            });

            builder.Bind<Client>().To(new Client(
                Guid.NewGuid(),
                PreConfiguration.ClientPort,
                channel
            )).Singleton();
        }
    }
}