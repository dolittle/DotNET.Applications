// Copyright (c) Dolittle. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

extern alias contracts;

using System;
using System.Net;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;
using contracts::Dolittle.Runtime.Heads;
using Dolittle.Booting;
using Dolittle.Logging;
using Dolittle.Protobuf;
using Dolittle.Services;
using static contracts::Dolittle.Runtime.Heads.Heads;

namespace Dolittle.Heads
{
    /// <summary>
    /// Performs boot procedures related to client.
    /// </summary>
    public class BootProcedure : ICanPerformBootProcedure
    {
        readonly Head _head;
        readonly HeadsClient _headsClient;
        readonly ILogger _logger;
        readonly IBoundServices _boundServices;

        /// <summary>
        /// Initializes a new instance of the <see cref="BootProcedure"/> class.
        /// </summary>
        /// <param name="head"><see cref="Head"/> representing the running client.</param>
        /// <param name="headsClient">The <see cref="HeadsClient"/>.</param>
        /// <param name="boundServices"><see cref="IBoundServices"/>.</param>
        /// <param name="logger"><see cref="ILogger"/> for logging.</param>
        public BootProcedure(
            Head head,
            HeadsClient headsClient,
            IBoundServices boundServices,
            ILogger logger)
        {
            _head = head;
            _headsClient = headsClient;
            _logger = logger;
            _boundServices = boundServices;
        }

        /// <inheritdoc/>
        public bool CanPerform() => true;

        /// <inheritdoc/>
        public void Perform()
        {
            _logger.Information($"Connect client '{_head.Id}'");
            var headId = _head.Id.ToProtobuf();
            var headInfo = new HeadInfo
            {
                HeadId = headId,
                Host = Dns.GetHostName(),
                Runtime = $".NET Core : {Environment.Version} - {Environment.OSVersion} - {Environment.ProcessorCount} cores",
                Version = Assembly.GetEntryAssembly().GetName().Version.ToString()
            };

            var streamCall = _headsClient.Connect(headInfo);
            Task.Run(async () =>
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
                    if (lastPing == DateTimeOffset.MinValue) return;
                    var delta = DateTimeOffset.UtcNow.Subtract(lastPing);
                    if (delta.TotalSeconds > 2) cancellationTokenSource.Cancel();
                };
                timer.Start();

                try
                {
                    while (await streamCall.ResponseStream.MoveNext(cancellationTokenSource.Token).ConfigureAwait(false))
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