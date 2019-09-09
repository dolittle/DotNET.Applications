/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using System;
using System.Diagnostics;
using System.Linq;
using System.Runtime.Loader;
using Dolittle.Booting;
using Dolittle.Logging;
using Dolittle.Runtime.Application.Grpc;
using Dolittle.Services;
using Google.Protobuf;
using static Dolittle.Runtime.Application.Grpc.Client;

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
            var client = new ClientClient(_client.RuntimeChannel);
            var clientId = new System.Protobuf.guid
            {
                Value = ByteString.CopyFrom(_client.Id.Value.ToByteArray())
            };
            var clientInfo = new ClientInfo
            {
                ClientId = clientId,
                Host = Environment.MachineName,
                Port = _client.Port,
                Runtime = $".NET Core : {Environment.Version} - {Environment.OSVersion} - {Environment.ProcessorCount} cores"
            };

            if (_boundServices.HasFor(ApplicationClientServiceType.ServiceType))
            {
                var boundServices = _boundServices.GetFor(ApplicationClientServiceType.ServiceType);
                clientInfo.ServicesByName.Add(boundServices.Select(_ => _.Descriptor.FullName));
            }

            void Disconnect()
            {
                System.Console.WriteLine($"Disconnect client '{_client.Id}'");
                _logger.Information($"Disconnect client '{_client.Id}'");
                try
                {
                    client.Disconnect(clientInfo.ClientId);
                    System.Console.WriteLine($"Client '{_client.Id}' disconnected");
                }
                catch (Exception ex)
                {
                    System.Console.WriteLine($"Couldn't disconnect client '{_client.Id}' - {ex.Message}");
                }
            }

            Process.GetCurrentProcess().Exited += (e, s) => Disconnect();
            AppDomain.CurrentDomain.DomainUnload += (e, s) => Disconnect();
            AppDomain.CurrentDomain.ProcessExit += (e, s) => Disconnect();
            AssemblyLoadContext.Default.Unloading += (e) => Disconnect();
            Console.CancelKeyPress += (e, s) => Disconnect();

            client.Connect(clientInfo);
        }
    }
}