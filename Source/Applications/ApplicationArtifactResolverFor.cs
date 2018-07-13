/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using System;
using System.Collections.Generic;
using System.Linq;
using Dolittle.Artifacts;
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
        static IArtifactType _resolver = new T();

        /// <inheritdoc/>
        public IArtifactType ArtifactType => _resolver;

        readonly Dictionary<IApplicationArtifactIdentifier, Type> _AAIToType;
        readonly IApplicationArtifacts _applicationArtifacts;
        readonly ITypeFinder _typeFinder;
        readonly IArtifactTypeToTypeMaps _artifactTypeToTypeMaps;
        readonly ILogger _logger;

        readonly Type _artifactTypeToTypeMap;

        // <summary>
        /// Initialize a new instance of <see cref="ApplicationArtifactResolverForCommand"/>
        /// </summary>
        /// <param name="applicationArtifacts"></param>
        /// <param name="typeFinder"></param>
        /// <param name="logger"></param>
        public ApplicationArtifactResolverFor(
            IApplicationArtifacts applicationArtifacts,
            ITypeFinder typeFinder,
            IArtifactTypeToTypeMaps artifactTypeToTypeMaps,
            ILogger logger
        )
        {
            _applicationArtifacts = applicationArtifacts;
            _typeFinder = typeFinder;
            _artifactTypeToTypeMaps = artifactTypeToTypeMaps;
            _logger = logger;

            _artifactTypeToTypeMap = _artifactTypeToTypeMaps.Map(ArtifactType);

            _AAIToType = _typeFinder.FindMultiple(_artifactTypeToTypeMap).ToDictionary(c => _applicationArtifacts.Identify(c), c => c);
        }

        /// <inheritdoc/>
        public virtual Type Resolve(IApplicationArtifactIdentifier identifier) 
        {
            _logger.Trace($"Resolving an {typeof(IApplicationArtifactIdentifier)} in a {typeof(ICanResolveApplicationArtifacts).AssemblyQualifiedName} resolver that can resolve a {typeof(IApplicationArtifactIdentifier)} with {typeof(IArtifactType)} " + 
            $" {ArtifactType.Identifier} to a {typeof(Type).AssemblyQualifiedName} of type {_artifactTypeToTypeMap.AssemblyQualifiedName}");

            ThrowIfTypeNotFound(identifier);

            var matchedType = _AAIToType[identifier];

            _logger.Trace($"Resolved the {typeof(IApplicationArtifactIdentifier)} to {matchedType.AssemblyQualifiedName}");

            return matchedType;
        }

        void ThrowIfTypeNotFound(IApplicationArtifactIdentifier identifier)
        {
            if (_AAIToType.ContainsKey(identifier))
                throw new CouldNotResolveApplicationArtifactIdentifier(identifier, _artifactTypeToTypeMaps.Map(ArtifactType));
        }
    }
}