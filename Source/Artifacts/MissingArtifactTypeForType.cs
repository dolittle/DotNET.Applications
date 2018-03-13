/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using System;

namespace Dolittle.Artifacts
{

    /// <summary>
    /// Exception that gets thrown when there is no <see cref="IArtifactType"/> for a <see cref="Type"/>
    /// </summary>
    public class MissingArtifactTypeForType : Exception
    {
        /// <summary>
        /// Initializes a new instance of <see cref="MissingArtifactTypeForType"/>
        /// </summary>
        /// <param name="type"><see cref="Type"/> that has a missing <see cref="IArtifactType"/> map</param>
        public MissingArtifactTypeForType(Type type): base($"Type '{type.AssemblyQualifiedName} is not possible to map to an ArtifactType'")
        { }

    }
}