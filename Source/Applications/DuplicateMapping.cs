/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using System;

namespace Dolittle.Applications
{
    /// <summary>
    /// Exception that gets thrown when mapping <see cref="Types"/> and <see cref="IApplicationArtifactIdentifier"/>
    /// and the mapping already exists.
    /// </summary>
    public class DuplicateMapping : ArgumentException
    {
        /// <summary>
        /// Initializes a new instance of <see cref="DuplicateMapping"/>
        /// </summary>
        /// <param name="identifier">The <see cref="IApplicationArtifactIdentifier"/> that is mapped to multiple types</param>
        /// <param name="type">The <see cref="Type"/> the identifier fails to be mapped to</param>
        public DuplicateMapping(IApplicationArtifactIdentifier identifier, Type type)
            :base($"The artifact '{identifier.Artifact.Name}' with ArtifactType {identifier.Artifact.Type.Identifier} is mapped to multiple Types. " +
                $"There cannot be more than one artifact of the same ArtifactType with the same ArtifactName at the same location.")
        { }
        /// <summary>
        /// Initializes a new instance of <see cref="DuplicateMapping"/>
        /// </summary>
        /// <param name="type">The <see cref="Type"/> that is mapped to multiple <see cref="IApplicationArtifactIdentifier"/></param>
        /// <param name="identifier">The <see cref="IApplicationArtifactIdentifier"/> the type fails to be mapped to</param>
        public DuplicateMapping(Type type, IApplicationArtifactIdentifier identifier)
            :base($"The Type '{type.AssemblyQualifiedName}' is mapped to multiple IApplicationArtifacts. " + 
            $"There cannot be more than one artifact of the same ArtifactType with the same ArtifactName at the same location.'")
        { }
    }
}
