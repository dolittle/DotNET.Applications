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
    /// Represents an implementation of <see cref="IApplicationRuntimeServices"/>
    /// </summary>
    [Singleton]
    public class ApplicationRuntimeServices : IApplicationRuntimeServices
    {
        /// <summary>
        /// Initializes a new instance of <see cref="ApplicationRuntimeServices"/>
        /// </summary>
        /// <param name="definers">Instances of <see cref="IDefineApplicationRuntimeServices">definers</see></param>
        public ApplicationRuntimeServices(IInstancesOf<IDefineApplicationRuntimeServices> definers)
        {
            Services = definers.SelectMany(_ => _.Services);
        }

        /// <inheritdoc/>
        public IEnumerable<RuntimeServiceDefinition> Services { get; }
    }
}