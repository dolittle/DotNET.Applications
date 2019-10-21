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
using Dolittle.Services;
using Dolittle.Protobuf;
using Dolittle.Heads.Runtime;
using static Dolittle.Heads.Runtime.Heads;

namespace Dolittle.Heads
{
    /// <summary>
    /// Performs boot procedures related to client
    /// </summary>
    public class BootProcedure : ICanPerformBootProcedure
    {
        readonly Head _head;
        readonly ILogger _logger;
        readonly IBoundServices _boundServices;

        /// <summary>
        /// Initalizes a new instance of <see cref="BootProcedure"/>
        /// </summary>
        /// <param name="head"><see cref="Head"/> representing the running client</param>
        /// <param name="boundServices"><see cref="IBoundServices"/></param>
        /// <param name="logger"><see cref="ILogger"/> for logging</param>
        public BootProcedure(
            Head head,
            IBoundServices boundServices,
            ILogger logger)
        {
            _head = head;
            _logger = logger;
            _boundServices = boundServices;
        }

        /// <inheritdoc/>
        public bool CanPerform() => true;

        /// <inheritdoc/>
        public void Perform()
        {
            _logger.Information($"Connect client '{_head.Id}'");
            var head = new HeadsClient(_head.CallInvoker);
            var headId = _head.Id.ToProtobuf();
            var headInfo = new HeadInfo
            {
                HeadId = headId,
                Host = Environment.MachineName,
                Port = _head.Port,
                Runtime = $".NET Core : {Environment.Version} - {Environment.OSVersion} - {Environment.ProcessorCount} cores"
            };

            if (_boundServices.HasFor(ApplicationServiceType.ServiceType))
            {
                var boundServices = _boundServices.GetFor(ApplicationServiceType.ServiceType);
                headInfo.ServicesByName.Add(boundServices.Select(_ => _.Descriptor.FullName));
            }

            var streamCall = head.Connect(headInfo);
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

            Head.Connected = true;
        }
    }
}