// Copyright (c) Dolittle. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using Dolittle.Logging;
using Dolittle.Resilience;
using Grpc.Core;
using Polly;

namespace Dolittle.Heads
{
    /// <summary>
    /// Defines the policy for <see cref="Head"/>.
    /// </summary>
    public class HeadPolicy : IDefineAsyncPolicyForType
    {
        readonly ILogger _logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="HeadPolicy"/> class.
        /// </summary>
        /// <param name="logger"><see cref="ILogger"/> for logging.</param>
        public HeadPolicy(ILogger logger)
        {
            _logger = logger;
        }

        /// <inheritdoc/>
        public Type Type => typeof(Head);

        /// <inheritdoc/>
        public Polly.IAsyncPolicy Define() =>
          Polly.Policy
            .Handle<RpcException>(_ =>
            {
                if (_.Status.StatusCode != StatusCode.Cancelled)
                {
                    _logger.Error(_, "Problems connecting head to runtime");
                }

                return true;
            })
            .Or<Exception>(_ =>
            {
                _logger.Error(_, "Problems connecting head to runtime");
                return true;
            })
            .WaitAndRetryForeverAsync(_ => TimeSpan.FromSeconds(1));
    }
}