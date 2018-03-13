/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using Dolittle.Collections;
using Dolittle.Types;

namespace Dolittle.Artifacts
{
    /// <summary>
    /// Represents an implementation of <see cref="IArtifactTypeToTypeMaps"/>
    /// </summary>
    public class ArtifactTypeToTypeMaps : IArtifactTypeToTypeMaps
    {
        readonly IInstancesOf<ICanProvideArtifactTypeToTypeMaps> _artifactTypeToTypeMapProviders;
        readonly Dictionary<string, Type> _artifactTypeToTypeMaps = new Dictionary<string, Type>();
        readonly Dictionary<Type, IArtifactType> _typeToArtifactTypeMaps = new Dictionary<Type, IArtifactType>();

        /// <summary>
        /// Initializes a new instance of <see cref="ArtifactTypeToTypeMaps"/>
        /// </summary>
        /// <param name="artifactTypeToTypeMapProviders"></param>
        public ArtifactTypeToTypeMaps(IInstancesOf<ICanProvideArtifactTypeToTypeMaps> artifactTypeToTypeMapProviders)
        {
            _artifactTypeToTypeMapProviders = artifactTypeToTypeMapProviders;
            Populate();
        }

        /// <inheritdoc/>
        public IArtifactType Map(Type type)
        {
            ThrowIfMissingArtifactType(type);
            var underlyingType = _typeToArtifactTypeMaps.Keys.Single(t => t.IsAssignableFrom(type));
            return _typeToArtifactTypeMaps[underlyingType];
        }


        /// <inheritdoc/>
        public Type Map(IArtifactType type)
        {
            ThrowIfMissingType(type);
            return _artifactTypeToTypeMaps[type.Identifier];
        }

        void Populate()
        {
            _artifactTypeToTypeMapProviders.ForEach(provider =>
            {
                var maps = provider.Provide();
                maps.ForEach(map =>
                {
                    _artifactTypeToTypeMaps[map.ArtifactType.Identifier] = map.Type;
                    _typeToArtifactTypeMaps[map.Type] = map.ArtifactType;
                });
            });
        }

        void ThrowIfMissingType(IArtifactType type)
        {
            if (!_artifactTypeToTypeMaps.ContainsKey(type.Identifier)) throw new MissingTypeForArtifactType(type);
        }        

        void ThrowIfMissingArtifactType(Type type)
        {
            if (!_typeToArtifactTypeMaps.Keys.Any(t => t.IsAssignableFrom(type))) throw new MissingArtifactTypeForType(type);
        }        
    }
}