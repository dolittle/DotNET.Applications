/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using System;
using System.Collections.Generic;

namespace Dolittle.Artifacts
{
    /// <summary>
    /// Represents a mapping between <see cref="IArtifactType"/> and a <see cref="Type">CLR type</see>
    /// </summary>
    public class ArtifactTypeToType
    {
        /// <summary>
        /// Initializes a new instance of <see cref="ArtifactTypeToType"/>
        /// </summary>
        /// <param name="artifactType">The <see cref="IArtifactType"/></param>
        /// <param name="type">The <see cref="Type"/></param>
        public ArtifactTypeToType(IArtifactType artifactType, Type type)
        {
            ArtifactType = artifactType;
            Type = type;
        }

        /// <summary>
        /// Gets the <see cref="IArtifactType"/>
        /// </summary>
        public IArtifactType   ArtifactType { get; }

        /// <summary>
        /// Gets the CLR <see cref="Type"/> representing the <see cref="IArtifactType"/>
        /// </summary>
        public Type Type { get; }       
    }
}