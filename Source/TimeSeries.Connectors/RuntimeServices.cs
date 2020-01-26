// Copyright (c) Dolittle. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System.Collections.Generic;
using Dolittle.Heads;
using grpc = Dolittle.TimeSeries.Connectors.Runtime;

namespace Dolittle.TimeSeries.Connectors
{
    /// <summary>
    /// Represents the runtime services having a client representation.
    /// </summary>
    public class RuntimeServices : IDefineRuntimeServices
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="RuntimeServices"/> class.
        /// </summary>
        public RuntimeServices()
        {
            Services = new[]
            {
                new RuntimeServiceDefinition(typeof(grpc.PullConnectors.PullConnectorsClient), grpc.PullConnectors.Descriptor),
                new RuntimeServiceDefinition(typeof(grpc.PushConnectors.PushConnectorsClient), grpc.PullConnectors.Descriptor)
            };
        }

        /// <inheritdoc/>
        public IEnumerable<RuntimeServiceDefinition> Services { get; }
    }
}