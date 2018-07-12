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
        readonly IApplicationStructureMap _applicationStructureMap;
        readonly IArtifactTypes _types;
        readonly ITypeFinder _typeFinder;
        readonly IEnumerable<IArtifactType> _artifactTypes;
        readonly ILogger _logger;
        readonly Dictionary<string, ICanResolveApplicationArtifacts> _resolversByType;
        readonly IArtifactTypeToTypeMaps _artifactTypeToTypeMaps;

        /// <summary>
        /// Initializes a new instance of <see cref="ApplicationArtifactResolver"/>
        /// </summary>
        /// <param name="applicationStructureMap">Current <see cref="IApplicationStructureMap">application structure map</see></param>
        /// <param name="types"><see cref="IArtifactTypes">Artifact types</see> available</param>
        /// <param name="artifactTypeToTypeMaps"><see cref="IArtifactTypeToTypeMaps"/> for mapping between <see cref="IArtifactType"/> and <see cref="Type"/></param>
        /// <param name="resolvers">Instances of <see cref="ICanResolveApplicationArtifacts"/> for specialized resolving</param>
        /// <param name="typeFinder"><see cref="ITypeFinder"/> for discovering types needed</param>
        /// <param name="logger"><see cref="ILogger"/> for logging</param>
        public ApplicationArtifactResolver(
            IApplicationStructureMap applicationStructureMap,
            IArtifactTypes types,
            IArtifactTypeToTypeMaps artifactTypeToTypeMaps,
            IInstancesOf<ICanResolveApplicationArtifacts> resolvers,
            ITypeFinder typeFinder,
            IInstancesOf<IArtifactType> artifactTypes,
            ILogger logger)
        {
            _applicationStructureMap = applicationStructureMap;
            _types = types;
            _resolversByType = resolvers.ToDictionary(r => r.ArtifactType.Identifier, r => r);
            _typeFinder = typeFinder;
            _artifactTypes = artifactTypes;
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


            // var artifactType = _artifactTypeToTypeMaps.Map(identifier.Artifact.Type);
            // if (artifactType != null)
            // {
            //     var types = _typeFinder.FindMultiple(artifactType);
            //     var typesMatchingName = types.Where(t => t.Name == identifier.Artifact.Name);
            //     Type matchedType = null;

            //     if (_applicationStructureMap.DoesAnyFitInStructure(typesMatchingName))
            //         matchedType = _applicationStructureMap.GetBestMatchingTypeFor(typesMatchingName);

            //     ThrowIfMismatchedArtifactType(artifactType, matchedType);
            //     if (matchedType != null) return matchedType;

            //     _logger.Error($"Unknown application resurce type : {identifier.Artifact.Type.Identifier}");
            // }

            // throw new UnknownArtifactType(identifier.Artifact.Type.Identifier);
        }

        void ThrowIfUnknownArtifactType(string typeIdentifier)
        {
            if (! _artifactTypes.Any(artifactType => artifactType.Identifier == typeIdentifier))
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