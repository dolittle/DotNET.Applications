// Copyright (c) Dolittle. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System.Collections.Generic;
using Dolittle.Resilience;
using Grpc.Core;

namespace Dolittle.Heads
{
    /// <summary>
    /// Represents a resilient <see cref="ChannelBase"/>.
    /// </summary>
    public class ResilientChannel : ChannelBase
    {
        readonly Channel _channel;
        readonly IPolicyFor<ResilientCallInvoker> _policy;

        /// <summary>
        /// Initializes a new instance of the <see cref="ResilientChannel"/> class.
        /// </summary>
        /// <param name="host">Host to connect to.</param>
        /// <param name="port">Port to connect to.</param>
        /// <param name="credentials"><see cref="ChannelCredentials"/> to use.</param>
        /// <param name="options">All <see cref="ChannelOption">options</see>.</param>
        /// <param name="policy"><see cref="IPolicyFor{T}"/> for the <see cref="ResilientCallInvoker"/> to use.</param>
        public ResilientChannel(
            string host,
            int port,
            ChannelCredentials credentials,
            IEnumerable<ChannelOption> options,
            IPolicyFor<ResilientCallInvoker> policy)
            : base($"{host}:{port}")
        {
            _channel = new Channel(host, port, credentials, options);
            _policy = policy;
        }

        /// <inheritdoc/>
        public override CallInvoker CreateCallInvoker()
        {
            return _channel.CreateCallInvoker();
        }
    }
}