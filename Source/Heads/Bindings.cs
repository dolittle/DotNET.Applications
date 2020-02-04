// Copyright (c) Dolittle. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using Dolittle.DependencyInversion;
using Dolittle.Logging;
using Dolittle.Resilience;
using Grpc.Core;
using Grpc.Core.Interceptors;

namespace Dolittle.Heads
{
    /// <summary>
    /// Provides bindings related to client.
    /// </summary>
    public class Bindings : ICanProvideBindings
    {
        readonly GetContainer _getContainer;

        /// <summary>
        /// Initializes a new instance of the <see cref="Bindings"/> class.
        /// </summary>
        /// <param name="getContainer"><see cref="GetContainer"/> for getting the correct <see cref="IContainer"/>.</param>
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
                var logger = _getContainer().Get<ILogger>();

                logger.Information($"Connect head to runtime at '{configuration.Host}:{configuration.Port}'");

                var channel = new Channel(
                    configuration.Host,
                    configuration.Port,
                    ChannelCredentials.Insecure,
                    new[] { keepAliveTime, keepAliveTimeout, keepAliveWithoutCalls });

                var clientId = Guid.NewGuid();

                var callInvoker = channel.Intercept(_ =>
                {
                    _.Add(new Metadata.Entry("clientid", clientId.ToString()));
                    return _;
                });

                return new Head(
                    clientId,
                    PreConfiguration.ClientPort,
                    callInvoker);
            }).Singleton();
        }
    }
}