/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using System;
using System.Collections.Generic;
using System.Linq;
using Dolittle.Artifacts;
using Dolittle.Execution;
using Dolittle.Logging;
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
            ILogger logger)
        {
            _applicationStructureMap = applicationStructureMap;
            _types = types;
            _resolversByType = resolvers.ToDictionary(r => r.ArtifactType.Identifier, r => r);
            _typeFinder = typeFinder;
            _logger = logger;
            _artifactTypeToTypeMaps = artifactTypeToTypeMaps;
        }

        /// <inheritdoc/>
        public Type Resolve(IApplicationArtifactIdentifier identifier)
        {
            _logger.Trace($"Trying to resolve : {identifier.Artifact.Name} - with type {identifier.Artifact.Type.Identifier}");

            var typeIdentifier = identifier.Artifact.Type.Identifier;
            if (_resolversByType.ContainsKey(typeIdentifier)) return _resolversByType[typeIdentifier].Resolve(identifier);

            var artifactType = _artifactTypeToTypeMaps.Map(identifier.Artifact.Type);
            var types = _typeFinder.FindMultiple(artifactType);
            var typesMatchingName = types.Where(t => t.Name == identifier.Artifact.Name);

            if( _applicationStructureMap.DoesAnyFitInStructure(typesMatchingName))
                return _applicationStructureMap.GetBestMatchingTypeFor(typesMatchingName);

            _logger.Error($"Unknown application resurce type : {identifier.Artifact.Type.Identifier}");
            
            throw new UnknownArtifactType(identifier.Artifact.Type.Identifier);
        }
    }
}
