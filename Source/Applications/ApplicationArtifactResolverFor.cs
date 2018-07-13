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
        readonly ILogger _logger;

        // <summary>
        /// Initialize a new instance of <see cref="ApplicationArtifactResolverForCommand"/>
        /// </summary>
        /// <param name="applicationArtifacts"></param>
        /// <param name="typeFinder"></param>
        /// <param name="logger"></param>
        public ApplicationArtifactResolverFor(
            IApplicationArtifacts applicationArtifacts,
            ITypeFinder typeFinder,
            ILogger logger
        )
        {
            _applicationArtifacts = applicationArtifacts;
            _typeFinder = typeFinder;
            _logger = logger;

            _AAIToType = _typeFinder.FindMultiple(typeof(T)).ToDictionary(c => _applicationArtifacts.Identify(c), c => c);
        }

        /// <inheritdoc/>
        public virtual Type Resolve(IApplicationArtifactIdentifier identifier) 
        {
            _logger.Trace($"Resolving an {typeof(IApplicationArtifactIdentifier)} for the {typeof(IArtifactType)} of {}");

            ThrowIfTypeNotFound(identifier);

            var matchedType = _AAIToType[identifier];

            _logger.Trace($"Successfully resolved the {typeof(IApplicationArtifactIdentifier)} to {matchedType.AssemblyQualifiedName}");

            return matchedType;
        }

        void ThrowIfTypeNotFound(IApplicationArtifactIdentifier identifier)
        {
            if (_AAIToType.ContainsKey(identifier))
                throw new CommandNotFound(identifier);
        }
    }
}