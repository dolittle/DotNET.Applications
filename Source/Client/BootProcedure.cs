/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using System;
using System.Diagnostics;
using System.Runtime.Loader;
using Dolittle.Booting;
using Dolittle.Hosting;
using Dolittle.Logging;
using Dolittle.Runtime.Application.Grpc;
using Google.Protobuf;
using Grpc.Core;
using Grpc.Core.Interceptors;
using static Dolittle.Runtime.Application.Grpc.Client;

namespace Dolittle.Client
{
    /// <summary>
    /// Performs boot procedures related to client
    /// </summary>
    public class BootProcedure : ICanPerformBootProcedure
    {
        readonly ClientId _clientId;
        readonly ILogger _logger;
        readonly ClientPort _port;
        readonly CallLogger _callLogger;

        /// <summary>
        /// Initalizes a new instance of <see cref="BootProcedure"/>
        /// </summary>
        /// <param name="clientId"><see cref="ClientId"/> representing the running client</param>
        /// <param name="port"><see cref="ClientPort"/> to use for runtime to connect back to</param>
        /// <param name="logger"><see cref="ILogger"/> for logging</param>
        /// <param name="callLogger"><see cref="CallLogger"/> for logging calls</param>
        public BootProcedure(
            ClientId clientId,
            ClientPort port,
            ILogger logger,
            CallLogger callLogger)
        {
            _clientId = clientId;
            _logger = logger;
            _port = port;
            _callLogger = callLogger;
        }

        /// <inheritdoc/>
        public bool CanPerform() => true;

        /// <inheritdoc/>
        public void Perform()
        {
            _logger.Information($"Connect client '{_clientId}'");

            var channel = new Channel("0.0.0.0", 50053, ChannelCredentials.Insecure);
            channel.Intercept(_callLogger);
            var client = new ClientClient(channel);
            var clientInfo = new ClientInfo();
            clientInfo.ClientId = new System.Protobuf.guid();
            clientInfo.ClientId.Value = ByteString.CopyFrom(_clientId.Value.ToByteArray());
            clientInfo.Host = Environment.MachineName;
            clientInfo.Port = _port;
            clientInfo.Runtime = $".NET Core : {Environment.Version} - {Environment.OSVersion} - {Environment.ProcessorCount} cores";

            void Disconnect()
            {
                channel = new Channel("0.0.0.0", 50053, ChannelCredentials.Insecure);
                client = new ClientClient(channel);

                System.Console.WriteLine($"Disconnect client '{_clientId}'");
                _logger.Information($"Disconnect client '{_clientId}'");
                try
                {
                    client.Disconnect(clientInfo.ClientId);
                    System.Console.WriteLine($"Client '{_clientId}' disconnected");
                }
                catch (Exception ex)
                {
                    System.Console.WriteLine($"Couldn't disconnect client '{_clientId}' - {ex.Message}");
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