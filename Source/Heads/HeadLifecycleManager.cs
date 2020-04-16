// Copyright (c) Dolittle. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

extern alias contracts;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;
using contracts::Dolittle.Runtime.Heads;
using Dolittle.Collections;
using Dolittle.Lifecycle;
using Dolittle.Logging;
using Dolittle.Protobuf;
using Dolittle.Resilience;
using Dolittle.Types;
using static contracts::Dolittle.Runtime.Heads.Heads;

namespace Dolittle.Heads
{
    /// <summary>
    /// Represents an implementation of <see cref="IHeadLifecycleManager"/>.
    /// </summary>
    [Singleton]
    public class HeadLifecycleManager : IHeadLifecycleManager
    {
        readonly Head _head;
        readonly HeadsClient _headsClient;
        readonly IInstancesOf<ITakePartInHeadConnectionLifecycle> _headProcedures;
        readonly IAsyncPolicyFor<Head> _policy;
        readonly ILogger _logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="HeadLifecycleManager"/> class.
        /// </summary>
        /// <param name="head"><see cref="Head"/> representing the running client.</param>
        /// <param name="headsClient">The <see cref="HeadsClient"/>.</param>
        /// <param name="headProcedures"><see cref="IInstancesOf{T}"/> of <see cref="ITakePartInHeadConnectionLifecycle"/>.</param>
        /// <param name="policy"><see cref="IAsyncPolicyFor{T}"/> for <see cref="Head"/>.</param>
        /// <param name="logger"><see cref="ILogger"/> for logging.</param>
        public HeadLifecycleManager(
            Head head,
            HeadsClient headsClient,
            IInstancesOf<ITakePartInHeadConnectionLifecycle> headProcedures,
            IAsyncPolicyFor<Head> policy,
            ILogger logger)
        {
            _head = head;
            _headsClient = headsClient;
            _headProcedures = headProcedures;
            _policy = policy;
            _logger = logger;
        }

        /// <inheritdoc/>
        public bool IsReady() => _headProcedures.All(_ => _.IsReady());

        /// <inheritdoc/>
        public void Start()
        {
            _logger.Debug($"Connecting client '{_head.Id}'");
            var headId = _head.Id.ToProtobuf();
            var headInfo = new HeadInfo
            {
                HeadId = headId,

                Host = Dns.GetHostName(),
                Runtime = $".NET Core : {Environment.Version} - {Environment.OSVersion} - {Environment.ProcessorCount} cores",

                Version = Assembly.GetEntryAssembly().GetName().Version.ToString()
            };

            Task.Run(async () =>
            {
                await _policy.Execute(async () =>
                {
                    var streamCall = _headsClient.Connect(headInfo);
                    var cancellationTokenSource = new CancellationTokenSource();
                    cancellationTokenSource.Token.ThrowIfCancellationRequested();

                    var lastPing = DateTimeOffset.MinValue;

                    var timer = new System.Timers.Timer(10000)
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

                    bool connected = false;

                    try
                    {
                        IEnumerable<Task> tasks = null;
                        while (await streamCall.ResponseStream.MoveNext(cancellationTokenSource.Token).ConfigureAwait(false))
                        {
                            if (!connected)
                            {
                                tasks = OnConnected(cancellationTokenSource.Token);
                                connected = true;
                            }

                            lastPing = DateTimeOffset.UtcNow;

                            if (!cancellationTokenSource.IsCancellationRequested && tasks.Any(_ => _.IsFaulted))
                            {
                                tasks.Where(_ => _.IsFaulted).ForEach(_ => _logger.Error(_.Exception, $"Exception thrown in HeadConnectionLifecycle task: {GetInnermostException(_.Exception).Message}"));
                                cancellationTokenSource.Cancel();
                            }
                        }
                    }
                    finally
                    {
                        if (connected)
                        {
                            OnDisconnected();
                        }

                        timer.Stop();
                        timer.Dispose();
                    }
                }).ConfigureAwait(false);
            });
        }

        Exception GetInnermostException(Exception exception)
        {
            while (exception.InnerException != null) exception = exception.InnerException;
            return exception;
        }

        IEnumerable<Task> OnConnected(CancellationToken token)
        {
            _logger.Information($"Connected to runtime");
            return _headProcedures.Select(_ => _.OnConnected(token)).ToList();
        }

        void OnDisconnected()
        {
            _logger.Information($"Disconnected from runtime");
        }
    }
}