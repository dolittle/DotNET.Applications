// Copyright (c) Dolittle. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

extern alias contracts;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using contracts::Dolittle.Runtime.TimeSeries.Connectors;
using Dolittle.Collections;
using Dolittle.Lifecycle;
using Dolittle.Logging;
using Dolittle.Types;
using Grpc.Core;
using static contracts::Dolittle.Runtime.TimeSeries.Connectors.PushConnectors;

namespace Dolittle.TimeSeries.Connectors
{
    /// <summary>
    /// Represents an implementation of <see cref="IPushConnectors"/>.
    /// </summary>
    [Singleton]
    public class PushConnectors : IPushConnectors
    {
        readonly IDictionary<ConnectorId, IAmAPushConnector> _connectors;
        readonly PushConnectorsClient _pushConnectorsClient;
        readonly ILogger _logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="PushConnectors"/> class.
        /// </summary>
        /// <param name="pushConnectorsClient">The <see cref="PushConnectorsClient"/> for connecting to runtime.</param>
        /// <param name="connectors">Instances of <see cref="IAmAPushConnector"/>.</param>
        /// <param name="logger"><see cref="ILogger"/> for logging.</param>
        public PushConnectors(
            PushConnectorsClient pushConnectorsClient,
            IInstancesOf<IAmAPushConnector> connectors,
            ILogger logger)
        {
            _connectors = connectors.ToDictionary(_ => (ConnectorId)Guid.NewGuid(), _ => _);
            _pushConnectorsClient = pushConnectorsClient;
            _logger = logger;
        }

        /// <inheritdoc/>
        public IAmAPushConnector GetById(ConnectorId connectorId)
        {
            return _connectors[connectorId];
        }

        /// <inheritdoc/>
        public void Register()
        {
            _connectors.ForEach(_ =>
            {
                Task.Run(async () =>
                {
                    _logger.Information($"Registering '{_.Value.Name}' with id '{_.Key}'");

                    var metadata = new Metadata
                    {
                        { "pushconnectorid", _.Key.ToString() },
                        { "pushconnectorname", _.Value.Name }
                    };

                    try
                    {
                        var streamingCall = _pushConnectorsClient.Open(metadata);

                        await Process(_.Value, streamingCall.RequestStream).ConfigureAwait(false);
                    }
                    catch
                    {
                        Environment.Exit(1);
                    }
                });
            });
        }

        async Task Process(IAmAPushConnector connector, IClientStreamWriter<PushTagDataPoints> requestStream)
        {
            var streamWriter = new StreamWriter(requestStream);
            await connector.Connect(streamWriter).ConfigureAwait(false);
        }
    }
}