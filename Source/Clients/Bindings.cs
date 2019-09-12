/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using System;
using Dolittle.DependencyInversion;
using Dolittle.Resilience;
using Grpc.Core;

namespace Dolittle.Clients
{

    /// <summary>
    /// Provides bindings related to client
    /// </summary>
    public class Bindings : ICanProvideBindings
    {
        readonly GetContainer _getContainer;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="getContainer"></param>
        public Bindings(GetContainer getContainer)
        {
            _getContainer = getContainer;
        }

        /// <inheritdoc/>
        public void Provide(IBindingProviderBuilder builder)
        {
            builder.Bind<Client>().To(() =>
            {
                var keepAliveTime = new ChannelOption("grpc.keepalive_time", 1000);
                var keepAliveTimeout = new ChannelOption("grpc.keepalive_timeout_ms", 500);
                var keepAliveWithoutCalls = new ChannelOption("grpc.keepalive_permit_without_calls", 1);

                var configuration = _getContainer().Get<ClientConfiguration>();
                var policy = _getContainer().Get<IPolicyFor<ResilientCallInvoker>>();

                var channel = new Channel(configuration.Host, configuration.Port, ChannelCredentials.Insecure, new []
                {
                    keepAliveTime,
                    keepAliveTimeout,
                    keepAliveWithoutCalls
                });

                return new Client(
                    Guid.NewGuid(),
                    PreConfiguration.ClientPort,
                    channel
                );
            }).Singleton();
        }
    }
}