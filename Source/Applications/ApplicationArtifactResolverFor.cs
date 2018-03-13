/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using System;
using Dolittle.Artifacts;

namespace Dolittle.Applications
{
    /// <summary>
    /// Represents an implementation of <see cref="ICanResolveApplicationArtifacts"/>
    /// </summary>
    /// <typeparam name="T">Type of <see cref="IArtifactType"/> it suports</typeparam>
    public abstract class ApplicationArtifactResolverFor<T> : ICanResolveApplicationArtifacts
        where T: IArtifactType, new()
    {
        static IArtifactType _resolver = new T();

        /// <inheritdoc/>
        public IArtifactType ArtifactType => _resolver;

        /// <inheritdoc/>
        public abstract Type Resolve(IApplicationArtifactIdentifier identifier);
    }
}