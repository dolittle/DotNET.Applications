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
        /// <param name="identifier"></param>
        public DuplicateMapping(IApplicationArtifactIdentifier identifier)
            :base($"Ambiguous types found for identifier '{identifier.Artifact.Name}'")
        { }
    }
}
