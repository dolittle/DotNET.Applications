/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using System.Collections.Generic;
using Dolittle.Resilience;
using Grpc.Core;

namespace Dolittle.Clients
{
    /// <summary>
    /// 
    /// </summary>
    public class ResilientChannel : ChannelBase
    {
        readonly Channel _channel;
        readonly IPolicyFor<ResilientCallInvoker> _policy;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="host"></param>
        /// <param name="port"></param>
        /// <param name="credentials"></param>
        /// <param name="options"></param>
        /// <param name="policy"></param>
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