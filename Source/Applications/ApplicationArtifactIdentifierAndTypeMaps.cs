using System;
using System.Collections.Generic;
using System.Linq;
using Dolittle.Artifacts;
using Dolittle.Collections;
using Dolittle.Execution;
using Dolittle.Types;

namespace Dolittle.Applications
{
    /// <summary>
    /// Represents an implementation of <see cref="IApplicationArtifactIdentifierAndTypeMaps"/>
    /// </summary>
    [Singleton]
    public class ApplicationArtifactIdentifierAndTypeMaps : IApplicationArtifactIdentifierAndTypeMaps
    {
        /// <summary>
        /// Represents the one-to-one relationship between a <see cref="IApplicationArtifactIdentifier"/> and a <see cref="Type"/>
        /// </summary>
        readonly Dictionary<Type, IApplicationArtifactIdentifier> _typeToArtifactIdentifierMaps = new Dictionary<Type, IApplicationArtifactIdentifier>();
        readonly IApplication _application;
        readonly IApplicationLocationResolver _locationResolver;
        readonly IArtifactTypeToTypeMaps _artifactTypeToTypeMaps;
        readonly ITypeFinder _typeFinder;
        
        readonly object _initializedLock = new Object();

        bool _initialized = false;

        /// <summary>
        /// Initializes an instance of <see cref="ApplicationArtifactIdentifierAndTypeMaps"/>
        /// </summary>
        /// <param name="application"></param>
        /// <param name="locationResolver"></param>
        /// <param name="typeToTypeMaps"></param>
        /// <param name="typeFinder"></param>
        public ApplicationArtifactIdentifierAndTypeMaps(
            IApplication application,
            IApplicationLocationResolver locationResolver,
            IArtifactTypeToTypeMaps typeToTypeMaps,
            ITypeFinder typeFinder
        )
        {
            _application = application;
            _locationResolver = locationResolver;
            _artifactTypeToTypeMaps = typeToTypeMaps;
            _typeFinder = typeFinder;

            EnsureInitialized();
        }


        /// <inheritdoc/>
        public IApplicationArtifactIdentifier GetIdentifierFor(Type type)
        {
            ThrowIfNoMap(type);
            return _typeToArtifactIdentifierMaps[type];
        }

        /// <inheritdoc/>
        public IApplicationArtifactIdentifier GetIdentifierFor(object resource)
        {
            var type = resource.GetType();
            return GetIdentifierFor(type);
        }

        /// <inheritdoc/>
        public Type GetTypeFor(IApplicationArtifactIdentifier artifactIdentifier)
        {
            ThrowIfNoMap(artifactIdentifier);
            ThrowIfMultipleMatchingTypes(artifactIdentifier);
            return _typeToArtifactIdentifierMaps.SingleOrDefault(pair => pair.Value.Equals(artifactIdentifier)).Key;
        }

        void EnsureInitialized()
        {
            if (_initialized) return;
            lock (_initializedLock)
            {
                if (! _initialized) 
                {
                    Populate();
                    _initialized = true;
                }
            }
        }

        void Populate()
        {
            foreach (var artifactTypeToTypeMap in _artifactTypeToTypeMaps.MappedTypes)
            {
                var artifactType = _artifactTypeToTypeMaps.Map(artifactTypeToTypeMap);

                _typeFinder.FindMultiple(artifactTypeToTypeMap).ForEach(
                    type => 
                    {   
                        var aai = new ApplicationArtifactIdentifier(_application, _locationResolver.Resolve(type), new Artifact(type.Name, artifactType, 1));

                        ThrowIfDuplicateMapping(type, aai);
                        _typeToArtifactIdentifierMaps.Add(type, aai);
                    }
                );
            }
        }

        void ThrowIfNoMap(Type type)
        {
            if (! _typeToArtifactIdentifierMaps.ContainsKey(type))
                throw new UnableToIdentifyArtifact(type);
        }
        void ThrowIfNoMap(IApplicationArtifactIdentifier artifactIdentifier)
        {
            if (! _typeToArtifactIdentifierMaps.Values.Any(aai => aai.Equals(artifactIdentifier)))
                throw new CouldNotResolveApplicationArtifactIdentifier(artifactIdentifier, _artifactTypeToTypeMaps.Map(artifactIdentifier.Artifact.Type));
        }
        
        void ThrowIfDuplicateMapping(Type type, IApplicationArtifactIdentifier aai)
        {
            if (_typeToArtifactIdentifierMaps.ContainsKey(type))
                throw new DuplicateMapping(aai);
        }

        void ThrowIfMultipleMatchingTypes(IApplicationArtifactIdentifier artifactIdentifier)
        {
            if (_typeToArtifactIdentifierMaps.Count(pair => pair.Value.Equals(artifactIdentifier)) > 1)
            {
                throw new MultipleTypesWithTheSameArtifactIdentifier(artifactIdentifier);   
            }
        }
    }
}