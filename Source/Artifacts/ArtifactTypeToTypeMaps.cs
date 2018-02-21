/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using doLittle.Collections;
using doLittle.Types;

namespace doLittle.Artifacts
{
    /// <summary>
    /// Represents an implementation of <see cref="IArtifactTypeToTypeMaps"/>
    /// </summary>
    public class ArtifactTypeToTypeMaps : IArtifactTypeToTypeMaps
    {
        readonly IInstancesOf<ICanProvideArtifactTypeToTypeMaps> _artifactTypeToTypeMapProviders;
        readonly Dictionary<IArtifactType, Type> _artifactTypeToTypeMaps = new Dictionary<IArtifactType, Type>();
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
            return _typeToArtifactTypeMaps[type];
        }


        /// <inheritdoc/>
        public Type Map(IArtifactType type)
        {
            ThrowIfMissingType(type);
            return _artifactTypeToTypeMaps[type];
        }

        void Populate()
        {
            _artifactTypeToTypeMapProviders.ForEach(provider =>
            {
                var maps = provider.Provide();
                maps.ForEach(map =>
                {
                    _artifactTypeToTypeMaps[map.ArtifactType] = map.Type;
                    _typeToArtifactTypeMaps[map.Type] = map.ArtifactType;
                });
            });
        }

        void ThrowIfMissingType(IArtifactType type)
        {
            if (!_artifactTypeToTypeMaps.ContainsKey(type)) throw new MissingTypeForArtifactType(type);
        }        

        void ThrowIfMissingArtifactType(Type type)
        {
            if (!_typeToArtifactTypeMaps.ContainsKey(type)) throw new MissingArtifactTypeForType(type);
        }        
    }
}