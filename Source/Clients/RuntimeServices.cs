/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using System.Collections.Generic;
using System.Linq;
using Dolittle.Lifecycle;
using Dolittle.Types;

namespace Dolittle.Clients
{
    /// <summary>
    /// Represents an implementation of <see cref="IRuntimeServices"/>
    /// </summary>
    [Singleton]
    public class RuntimeServices : IRuntimeServices
    {
        /// <summary>
        /// Initializes a new instance of <see cref="RuntimeServices"/>
        /// </summary>
        /// <param name="definers">Instances of <see cref="IDefineRuntimeServices">definers</see></param>
        public RuntimeServices(IInstancesOf<IDefineRuntimeServices> definers)
        {
            Services = definers.SelectMany(_ => _.Services);
        }

        /// <inheritdoc/>
        public IEnumerable<RuntimeServiceDefinition> Services { get; }
    }
}