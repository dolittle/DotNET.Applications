/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using Dolittle.Artifacts;
using Dolittle.Strings;

namespace Dolittle.Applications
{

    /// <summary>
    /// Exception that gets thrown when its not possible to resolve <see cref="IApplicationLocation"/> for a <see cref="Type"/>
    /// </summary>
    public class UnableToResolveApplicationLocationForType : Exception
    {
        /// <summary>
        /// Initializes a new instance of <see cref="UnableToResolveApplicationLocationForType"/>
        /// </summary>
        /// <param name="type"><see cref="Type"/> that was not possible to resolve</param>
        public UnableToResolveApplicationLocationForType(Type type) : base($"Unable to resolve application location for type {type.AssemblyQualifiedName}")
        {
        }
    }
}