/*---------------------------------------------------------------------------------------------
 *  Copyright (c) 2008-2017 doLittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using System;

namespace doLittle.Artifacts
{
    /// <summary>
    /// Exception that gets thrown if a specific <see cref="IArtifactType"/> is unknown
    /// </summary>
    public class UnknownArtifactType : ArgumentException
    {
        /// <summary>
        /// Initializes a new instance of <see cref="UnknownArtifactType"/>
        /// </summary>
        /// <param name="type"><see cref="Type">Type</see> of the resource type</param>
        public UnknownArtifactType(Type type) : base($"Unknown application artifact type of '{type.FullName}'")
        { }


        /// <summary>
        /// Initializes a new instance of <see cref="UnknownArtifactType"/>
        /// </summary>
        /// <param name="identifier"><see cref="string">Identifier</see> of the resource type</param>
        public UnknownArtifactType(string identifier) : base($"Unknown application artifact type of '{identifier}'")
        { }
    }
}
