/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using System.Threading.Tasks;
using System.Timers;
using Dolittle.Logging;
using Google.Protobuf.WellKnownTypes;
using Grpc.Core;
using static Dolittle.Runtime.Application.Grpc.Client.ConnectionStatus;

namespace Dolittle.Clients
{
    /// <summary>
    /// Represents an implementation of <see cref="ConnectionStatusBase"/>
    /// </summary>
    public class ConnectionStatusService : ConnectionStatusBase
    {
        readonly ILogger _logger;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="logger"></param>
        public ConnectionStatusService(ILogger logger)
        {
            _logger = logger;
        }

        /// <inheritdoc/>
        public override async Task Connect(IAsyncStreamReader<Empty> requestStream, IServerStreamWriter<Empty> responseStream, ServerCallContext context)
        {
            _logger.Information("Runtime connected");
            var timer = new Timer(1000);
            timer.Elapsed += (s,e) => responseStream.WriteAsync(new Empty());
            timer.Start();
            _logger.Information("Reading from server");
            while( await requestStream.MoveNext(context.CancellationToken))
            {
                //_logger.Information("Server pinged");
            }

            _logger.Information("Server disconnected");
        }
    }
}