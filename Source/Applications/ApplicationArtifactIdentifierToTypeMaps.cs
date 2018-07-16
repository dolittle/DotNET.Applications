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
    /// Represents an implementation of <see cref="IApplicationArtifactIdentifierToTypeMaps"/>
    /// </summary>
    [Singleton]
    public class ApplicationArtifactIdentifierToTypeMaps : IApplicationArtifactIdentifierToTypeMaps
    {
        readonly Dictionary<Type, IApplicationArtifactIdentifier> _typeToArtifactIdentifierMaps = new Dictionary<Type, IApplicationArtifactIdentifier>();

        readonly IApplication _application;
        readonly IApplicationLocationResolver _locationResolver;
        readonly IArtifactTypeToTypeMaps _artifactTypeToTypeMaps;
        readonly ITypeFinder _typeFinder;
        
        readonly object _initializedLock = new Object();

        bool _initialized = false;

        /// <summary>
        /// Initializes an instance of <see cref="ApplicationArtifactIdentifierToTypeMaps"/>
        /// </summary>
        /// <param name="application"></param>
        /// <param name="locationResolver"></param>
        /// <param name="typeToTypeMaps"></param>
        /// <param name="typeFinder"></param>
        public ApplicationArtifactIdentifierToTypeMaps(
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
        public IApplicationArtifactIdentifier Map(Type type)
        {
            ThrowIfNoMap(type);
            return _typeToArtifactIdentifierMaps[type];
        }

        /// <inheritdoc/>
        public IApplicationArtifactIdentifier Map(object resource)
        {
            var type = resource.GetType();
            return Map(type);
        }

        /// <inheritdoc/>
        public Type Map(IApplicationArtifactIdentifier artifactIdentifier)
        {
            ThrowIfNoMap(artifactIdentifier);
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

                        ThrowIfAmbiguousType(type, aai);
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
        
        void ThrowIfAmbiguousType(Type type, IApplicationArtifactIdentifier aai)
        {
            if (_typeToArtifactIdentifierMaps.ContainsKey(type))
                throw new AmbiguousTypes(aai);
        }
    }
}