/*---------------------------------------------------------------------------------------------
 *  Copyright (c) 2008-2017 Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using System;
using Dolittle.Artifacts;

namespace Dolittle.Applications
{
    /// <summary>
    /// Defines a system that can resolve <see cref="IArtifact">application artifact</see>
    /// typically from <see cref="IApplicationArtifactIdentifier"/> to a concrete <see cref="Type"/>
    /// </summary>
    public interface IApplicationArtifactResolver
    {
        /// <summary>
        /// Resolve a <see cref="IApplicationArtifactIdentifier"/> into a <see cref="Type"/>
        /// </summary>
        /// <param name="identifier"><see cref="IApplicationArtifactIdentifier"/> to resolve</param>
        /// <returns>Resolved <see cref="Type"/></returns>
        Type Resolve(IApplicationArtifactIdentifier identifier);
    }
}