// Copyright (c) Dolittle. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System.Collections.Generic;
using System.Linq;
using Dolittle.Lifecycle;
using Dolittle.Types;

namespace Dolittle.Heads
{
    /// <summary>
    /// Represents an implementation of <see cref="IRuntimeServices"/>.
    /// </summary>
    [Singleton]
    public class RuntimeServices : IRuntimeServices
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="RuntimeServices"/> class.
        /// </summary>
        /// <param name="definers">Instances of <see cref="IDefineRuntimeServices">definers</see>.</param>
        public RuntimeServices(IInstancesOf<IDefineRuntimeServices> definers)
        {
            Services = definers.SelectMany(_ => _.Services);
        }

        /// <inheritdoc/>
        public IEnumerable<RuntimeServiceDefinition> Services { get; }
    }
}