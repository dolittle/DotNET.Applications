/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using System;
using Dolittle.DependencyInversion;
using Dolittle.Resilience;
using Grpc.Core;
using Grpc.Core.Interceptors;

namespace Dolittle.Heads
{
    /// <summary>
    /// Provides bindings related to client
    /// </summary>
    public class Bindings : ICanProvideBindings
    {
        readonly GetContainer _getContainer;

        /// <summary>
        /// Initializes a new instance of <see cref="Bindings"/>
        /// </summary>
        /// <param name="getContainer"><see cref="GetContainer"/> for getting the correct <see cref="IContainer"/>></param>
        public Bindings(GetContainer getContainer)
        {
            _getContainer = getContainer;
        }

        /// <inheritdoc/>
        public void Provide(IBindingProviderBuilder builder)
        {
            builder.Bind<Head>().To(() =>
            {
                var keepAliveTime = new ChannelOption("grpc.keepalive_time", 1000);
                var keepAliveTimeout = new ChannelOption("grpc.keepalive_timeout_ms", 500);
                var keepAliveWithoutCalls = new ChannelOption("grpc.keepalive_permit_without_calls", 1);

                var configuration = _getContainer().Get<RuntimeConfiguration>();
                var policy = _getContainer().Get<IPolicyFor<ResilientCallInvoker>>();

                var channel = new Channel(configuration.Host, configuration.Port, ChannelCredentials.Insecure, new []
                {
                    keepAliveTime,
                    keepAliveTimeout,
                    keepAliveWithoutCalls
                });

                var clientId = Guid.NewGuid();

                var callInvoker = channel.Intercept(_ => {
                    _.Add(new Metadata.Entry("clientid", clientId.ToString()));
                    return _;
                });

                var client = new Head(
                    clientId,
                    PreConfiguration.ClientPort,
                    callInvoker
                );

                return client;
            }).Singleton();
        }
    }
}