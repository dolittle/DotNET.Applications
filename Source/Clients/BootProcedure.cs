/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Dolittle.Booting;
using Dolittle.Logging;
using Dolittle.Applications;
using Dolittle.Services;
using Google.Protobuf;
using static Dolittle.Applications.Runtime.Clients;
using Dolittle.Protobuf;

namespace Dolittle.Clients
{
    /// <summary>
    /// Performs boot procedures related to client
    /// </summary>
    public class BootProcedure : ICanPerformBootProcedure
    {
        readonly Client _client;
        readonly ILogger _logger;
        readonly IBoundServices _boundServices;

        /// <summary>
        /// Initalizes a new instance of <see cref="BootProcedure"/>
        /// </summary>
        /// <param name="client"><see cref="Client"/> representing the running client</param>
        /// <param name="logger"><see cref="ILogger"/> for logging</param>
        /// <param name="boundServices"></param>
        public BootProcedure(
            Client client,
            ILogger logger,
            IBoundServices boundServices)
        {
            _client = client;
            _logger = logger;
            _boundServices = boundServices;
        }

        /// <inheritdoc/>
        public bool CanPerform() => true;

        /// <inheritdoc/>
        public void Perform()
        {
            _logger.Information($"Connect client '{_client.Id}'");
            var client = new ClientsClient(_client.CallInvoker);
            var clientId = _client.Id.ToProtobuf();
            var clientInfo = new ClientInfo
            {
                ClientId = clientId,
                Host = Environment.MachineName,
                Port = _client.Port,
                Runtime = $".NET Core : {Environment.Version} - {Environment.OSVersion} - {Environment.ProcessorCount} cores"
            };

            if (_boundServices.HasFor(ApplicationServiceType.ServiceType))
            {
                var boundServices = _boundServices.GetFor(ApplicationServiceType.ServiceType);
                clientInfo.ServicesByName.Add(boundServices.Select(_ => _.Descriptor.FullName));
            }

            var streamCall = client.Connect(clientInfo);
            Task.Run(async() =>
            {
                var cancellationTokenSource = new CancellationTokenSource();
                cancellationTokenSource.Token.ThrowIfCancellationRequested();

                var lastPing = DateTimeOffset.MinValue;

                var timer = new System.Timers.Timer(2000)
                {
                    Enabled = true
                };
                timer.Elapsed += (s, e) =>
                {
                    if( lastPing == DateTimeOffset.MinValue ) return;
                    var delta = DateTimeOffset.UtcNow.Subtract(lastPing);
                    if( delta.TotalSeconds > 2 ) cancellationTokenSource.Cancel();
                };
                timer.Start();

                try
                { 
                    while (await streamCall.ResponseStream.MoveNext(cancellationTokenSource.Token))
                    {
                        lastPing = DateTimeOffset.UtcNow;
                    }
                }
                finally
                {
                    _logger.Information("Server disconnected");
                }
            });

            Client.Connected = true;
        }
    }
}