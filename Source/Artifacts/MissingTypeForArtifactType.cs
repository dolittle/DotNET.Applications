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
    public class MissingTypeForArtifactType : Exception
    {
        /// <summary>
        /// Initializes a new instance of <see cref="MissingArtifactTypeForType"/>
        /// </summary>
        /// <param name="type"><see cref="IArtifactType"/> that has a missing <see cref="Type"/> map</param>
        public MissingTypeForArtifactType(IArtifactType type): base($"ArtifactType '{type.Identifier} is not possible to map to a CLR Type'")
        { }

    }
}