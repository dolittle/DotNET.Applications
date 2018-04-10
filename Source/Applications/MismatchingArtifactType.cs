/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using System;
using Dolittle.Artifacts;

namespace Dolittle.Applications
{
    /// <summary>
    /// Exception that gets thrown when a <see cref="Type"/> does not match a type specified in <see cref="IArtifactTypeMapFor{T}"/>
    /// </summary>
    public class MismatchingArtifactType : Exception
    {
        /// <summary>
        /// Initializes a new instance of <see cref="MismatchingArtifactType"/>
        /// </summary>
        /// <param name="artifactType"><see cref="Type"/> of expected artifact</param>
        /// <param name="actualType"><see cref="Type"/> of the actual type</param>
        /// <returns></returns>
        public MismatchingArtifactType(Type artifactType, Type actualType) : base($"Expected type {actualType.AssemblyQualifiedName} to be of type {artifactType.AssemblyQualifiedName}")
        {
        }
    }
}
