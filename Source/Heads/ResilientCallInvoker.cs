// Copyright (c) Dolittle. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using Dolittle.Resilience;
using Grpc.Core;

namespace Dolittle.Heads
{
    /// <summary>
    /// Represents a resilient <see cref="CallInvoker"/>.
    /// </summary>
    public class ResilientCallInvoker : CallInvoker
    {
        readonly IPolicyFor<ResilientCallInvoker> _policy;
        private readonly CallInvoker _innerCallinvoker;

        /// <summary>
        /// Initializes a new instance of the <see cref="ResilientCallInvoker"/> class.
        /// </summary>
        /// <param name="policy"><see cref="IPolicyFor{T}"/> the invoker.</param>
        /// <param name="innerCallinvoker"><see cref="CallInvoker"/> to forward to.</param>
        public ResilientCallInvoker(
            IPolicyFor<ResilientCallInvoker> policy,
            CallInvoker innerCallinvoker)
        {
            _policy = policy;
            _innerCallinvoker = innerCallinvoker;
        }

        /// <inheritdoc/>
        public override AsyncClientStreamingCall<TRequest, TResponse> AsyncClientStreamingCall<TRequest, TResponse>(Method<TRequest, TResponse> method, string host, CallOptions options)
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc/>
        public override AsyncDuplexStreamingCall<TRequest, TResponse> AsyncDuplexStreamingCall<TRequest, TResponse>(Method<TRequest, TResponse> method, string host, CallOptions options)
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc/>
        public override AsyncServerStreamingCall<TResponse> AsyncServerStreamingCall<TRequest, TResponse>(Method<TRequest, TResponse> method, string host, CallOptions options, TRequest request)
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc/>
        public override AsyncUnaryCall<TResponse> AsyncUnaryCall<TRequest, TResponse>(Method<TRequest, TResponse> method, string host, CallOptions options, TRequest request)
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc/>
        public override TResponse BlockingUnaryCall<TRequest, TResponse>(Method<TRequest, TResponse> method, string host, CallOptions options, TRequest request)
        {
            throw new NotImplementedException();
        }
    }
}