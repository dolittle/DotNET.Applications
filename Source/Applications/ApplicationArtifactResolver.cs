/*---------------------------------------------------------------------------------------------
 *  Copyright (c) 2008-2017 doLittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using System;
using System.Collections.Generic;
using System.Linq;
using doLittle.Applications;
using doLittle.Artifacts;
using doLittle.Collections;
using doLittle.Execution;
using doLittle.Logging;
using doLittle.Types;

namespace doLittle.Applications
{
    /// <summary>
    /// Represents an implementation of <see cref="IApplicationArtifactResolver"/>
    /// </summary>
    [Singleton]
    public class ApplicationArtifactResolver : IApplicationArtifactResolver
    {
        IApplication _application;
        IArtifactTypes _types;
        ITypeFinder _typeFinder;
        ILogger _logger;
        Dictionary<string, ICanResolveApplicationArtifacts> _resolversByType;

        /// <summary>
        /// Initializes a new instance of <see cref="ApplicationArtifactResolver"/>
        /// </summary>
        /// <param name="application">Current <see cref="IApplication">Application</see></param>
        /// <param name="types"><see cref="IArtifactTypes">Artifact types</see> available</param>
        /// <param name="resolvers">Instances of <see cref="ICanResolveApplicationArtifacts"/> for specialized resolving</param>
        /// <param name="typeFinder"><see cref="ITypeFinder"/> for discovering types needed</param>
        /// <param name="logger"><see cref="ILogger"/> for logging</param>
        public ApplicationArtifactResolver(
            IApplication application, 
            IArtifactTypes types, 
            IInstancesOf<ICanResolveApplicationArtifacts> resolvers, 
            ITypeFinder typeFinder,
            ILogger logger)
        {
            _application = application;
            _types = types;
            _resolversByType = resolvers.ToDictionary(r => r.ArtifactType.Identifier, r => r);
            _typeFinder = typeFinder;
            _logger = logger;
        }

        /// <inheritdoc/>
        public Type Resolve(IApplicationArtifactIdentifier identifier)
        {
            _logger.Trace($"Trying to resolve : {identifier.Artifact.Name} - with type {identifier.Artifact.Type.Identifier}");

#if(false)
            var typeIdentifier = identifier.Artifact.Type.Identifier;
            if (_resolversByType.ContainsKey(typeIdentifier)) return _resolversByType[typeIdentifier].Resolve(identifier);

            var artifactType = _types.GetFor(typeIdentifier);
            var types = _typeFinder.FindMultiple(artifactType.Type);
            var typesMatchingName = types.Where(t => t.Name == identifier.Artifact.Name);

            ThrowIfAmbiguousTypes(identifier, typesMatchingName);

            var formats = _application.Structure.GetStructureFormatsForArea(artifactType.Area);
            var type = typesMatchingName.Where(t => formats.Any(f => f.Match(t.Namespace).HasMatches)).FirstOrDefault();
            if (type != null) return type;
#endif
            _logger.Error($"Unknown application resurce type : {identifier.Artifact.Type.Identifier}");
            
            throw new UnknownArtifactType(identifier.Artifact.Type.Identifier);
        }

        void ThrowIfAmbiguousTypes(IApplicationArtifactIdentifier identifier, IEnumerable<Type> typesMatchingName)
        {
            if (typesMatchingName.Count() > 1) 
            {
                _logger.Error($"Ambiguous types found for {identifier.Artifact.Name}");
                typesMatchingName.ForEach(type => _logger.Trace($"  Type found: {type.AssemblyQualifiedName}"));
                throw new AmbiguousTypes(identifier);
            }
        }
    }
}
