/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using System;
using System.Collections.Generic;
using System.Linq;
using Dolittle.Artifacts;
using Dolittle.Collections;
using Dolittle.Logging;
using Dolittle.Types;

namespace Dolittle.Applications
{
    /// <summary>
    /// Represents an implementation of <see cref="ICanResolveApplicationArtifacts"/>
    /// </summary>
    /// <typeparam name="T">Type of <see cref="IArtifactType"/> it suports</typeparam>
    public abstract class ApplicationArtifactResolverFor<T> : ICanResolveApplicationArtifacts
        where T: IArtifactType, new()
    {
        static readonly IArtifactType _resolver = new T();

        /// <inheritdoc/>
        public IArtifactType ArtifactType => _resolver;

        readonly IApplicationArtifactIdentifierToTypeMaps _aaiToTypeMaps;
        IArtifactTypeToTypeMaps _artifactTypeToTypeMaps;
        readonly ILogger _logger;
        
        readonly Type _artifactTypeToTypeMap;


        /// <summary>
        /// Initialize a new instance of <see cref="ApplicationArtifactResolverFor{T}"/>
        /// </summary>
        /// <param name="aaiToTypeMaps"></param>
        /// <param name="artifactTypeToTypeMaps"></param>
        /// <param name="logger"></param>
        public ApplicationArtifactResolverFor(
            IApplicationArtifactIdentifierToTypeMaps aaiToTypeMaps,
            IArtifactTypeToTypeMaps artifactTypeToTypeMaps,
            ILogger logger
        )
        {
            _aaiToTypeMaps = aaiToTypeMaps;
            _logger = logger;

            _artifactTypeToTypeMap = _artifactTypeToTypeMaps.Map(ArtifactType);

        }

        /// <inheritdoc/>
        public virtual Type Resolve(IApplicationArtifactIdentifier identifier) 
        {
            _logger.Trace($"Resolving an {typeof(IApplicationArtifactIdentifier)} in a {typeof(ICanResolveApplicationArtifacts).AssemblyQualifiedName} resolver that can resolve a {typeof(IApplicationArtifactIdentifier)} with {typeof(IArtifactType)} " + 
            $" {ArtifactType.Identifier} to a {typeof(Type).AssemblyQualifiedName} of type {_artifactTypeToTypeMap.AssemblyQualifiedName}");

            var matchedType = _aaiToTypeMaps.Map(identifier);

            _logger.Trace($"Resolved the {typeof(IApplicationArtifactIdentifier)} to {matchedType.AssemblyQualifiedName}");

            return matchedType;
        }

    }
}