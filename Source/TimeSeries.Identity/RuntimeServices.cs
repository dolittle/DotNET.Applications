// Copyright (c) Dolittle. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System.Collections.Generic;
using Dolittle.Heads;
using Dolittle.TimeSeries.Identity.Runtime;

namespace Dolittle.TimeSeries.Identity
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
                new RuntimeServiceDefinition(typeof(TimeSeriesMapIdentifier.TimeSeriesMapIdentifierClient), TimeSeriesMapIdentifier.Descriptor)
            };
        }

        /// <inheritdoc/>
        public IEnumerable<RuntimeServiceDefinition> Services { get; }
    }
}