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
        readonly IApplicationArtifactIdentifierAndTypeMaps _aaiAndTypeMaps;
        readonly IArtifactTypes _types;
        readonly ILogger _logger;
        readonly IArtifactTypeToTypeMaps _artifactTypeToTypeMaps;


        /// <summary>
        /// Initializes a new instance of <see cref="ApplicationArtifactResolver"/>
        /// </summary>
        /// 
        /// <param name="aaiAndTypeMaps">The maps between <see cref="Type"/> and <see cref="IApplicationArtifactIdentifier"/> </param>
        /// <param name="types"><see cref="IArtifactTypes">Artifact types</see> available</param>
        /// <param name="artifactTypeToTypeMaps"><see cref="IArtifactTypeToTypeMaps"/> for mapping between <see cref="IArtifactType"/> and <see cref="Type"/></param>
        /// <param name="logger"><see cref="ILogger"/> for logging</param>
        public ApplicationArtifactResolver(
            IApplicationArtifactIdentifierAndTypeMaps aaiAndTypeMaps,
            IArtifactTypes types,
            IArtifactTypeToTypeMaps artifactTypeToTypeMaps,
            ILogger logger)
        {
            _aaiAndTypeMaps = aaiAndTypeMaps;
            _types = types;
            _logger = logger;
            _artifactTypeToTypeMaps = artifactTypeToTypeMaps;
        }

        /// <inheritdoc/>
        public Type Resolve(IApplicationArtifactIdentifier identifier)
        {
            _logger.Trace($"Trying to resolve : {identifier.Artifact.Name} - with type {identifier.Artifact.Type.Identifier}");

            var typeIdentifier = identifier.Artifact.Type.Identifier;
            
            ThrowIfUnknownArtifactType(typeIdentifier);

            var matchedType = _aaiAndTypeMaps.GetTypeFor(identifier);

            _logger.Trace($"Matched {identifier.Artifact.Name} to a {matchedType.AssemblyQualifiedName}");

            ThrowIfMismatchedArtifactType(_artifactTypeToTypeMaps.Map(identifier.Artifact.Type), matchedType);

            return matchedType;
            
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