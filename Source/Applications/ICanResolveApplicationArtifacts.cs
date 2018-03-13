/*---------------------------------------------------------------------------------------------
 *  Copyright (c) 2008-2017 Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using System;
using Dolittle.Artifacts;

namespace Dolittle.Applications
{
    /// <summary>
    /// Defines a resolver that can resolve <see cref="Type">types</see> from <see cref="IApplicationArtifactIdentifier"/>
    /// </summary>
    public interface ICanResolveApplicationArtifacts
    {
        /// <summary>
        /// Gets the supported <see cref="IArtifactType"/>
        /// </summary>
        IArtifactType ArtifactType { get; }

        /// <summary>
        /// Resolve a <see cref="IApplicationArtifactIdentifier"/> into a <see cref="Type"/>
        /// </summary>
        /// <param name="identifier"><see cref="IApplicationArtifactIdentifier"/> to resolve</param>
        /// <returns>Resolved <see cref="Type"/></returns>
        Type Resolve(IApplicationArtifactIdentifier identifier);
    }
}