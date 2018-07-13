/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using Dolittle.Artifacts;
using Dolittle.Execution;
using Dolittle.Logging;
using Dolittle.Reflection;
using Dolittle.Types;

namespace Dolittle.Applications
{
    /// <summary>
    /// Represents an implementation of <see cref="IApplicationArtifactResolver"/>
    /// </summary>
    [Singleton]
    public class ApplicationArtifactResolver : IApplicationArtifactResolver
    {
        readonly IArtifactTypes _types;
        readonly ILogger _logger;
        readonly Dictionary<string, ICanResolveApplicationArtifacts> _resolversByType;
        readonly IArtifactTypeToTypeMaps _artifactTypeToTypeMaps;

        /// <summary>
        /// Initializes a new instance of <see cref="ApplicationArtifactResolver"/>
        /// </summary>
        /// <param name="types"><see cref="IArtifactTypes">Artifact types</see> available</param>
        /// <param name="artifactTypeToTypeMaps"><see cref="IArtifactTypeToTypeMaps"/> for mapping between <see cref="IArtifactType"/> and <see cref="Type"/></param>
        /// <param name="resolvers">Instances of <see cref="ICanResolveApplicationArtifacts"/> for specialized resolving</param>
        /// <param name="logger"><see cref="ILogger"/> for logging</param>
        public ApplicationArtifactResolver(
            IArtifactTypes types,
            IArtifactTypeToTypeMaps artifactTypeToTypeMaps,
            IInstancesOf<ICanResolveApplicationArtifacts> resolvers,
            ILogger logger)
        {
            _types = types;
            _resolversByType = resolvers.ToDictionary(r => r.ArtifactType.Identifier, r => r);
            _logger = logger;
            _artifactTypeToTypeMaps = artifactTypeToTypeMaps;
        }

        /// <inheritdoc/>
        public Type Resolve(IApplicationArtifactIdentifier identifier)
        {
            _logger.Trace($"Trying to resolve : {identifier.Artifact.Name} - with type {identifier.Artifact.Type.Identifier}");

            var typeIdentifier = identifier.Artifact.Type.Identifier;
            
            ThrowIfUnknownArtifactType(typeIdentifier);

            if (_resolversByType.ContainsKey(typeIdentifier)) 
            {
                var resolver = _resolversByType[typeIdentifier];
                var matchedType = resolver.Resolve(identifier);
                
                ThrowIfMismatchedArtifactType(_artifactTypeToTypeMaps.Map(resolver.ArtifactType), matchedType);

                return _resolversByType[typeIdentifier].Resolve(identifier);
            }
            throw new CouldNotFindResolver(typeIdentifier);
        }

        void ThrowIfUnknownArtifactType(string typeIdentifier)
        {
            if (! _types.Exists(typeIdentifier))
                throw new UnknownArtifactType(typeIdentifier);
        }

        void ThrowIfMismatchedArtifactType(Type artifactType, Type matchedType)
        {
            if (matchedType == null ||
                !artifactType.IsAssignableFrom(matchedType))
                throw new MismatchingArtifactType(artifactType, matchedType);
        }
    }
}