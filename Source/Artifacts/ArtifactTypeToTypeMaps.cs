/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using Dolittle.Collections;
using Dolittle.DependencyInversion;
using Dolittle.Types;

namespace Dolittle.Artifacts
{
    /// <summary>
    /// Represents an implementation of <see cref="IArtifactTypeToTypeMaps"/>
    /// </summary>
    public class ArtifactTypeToTypeMaps : IArtifactTypeToTypeMaps
    {
        readonly ITypeFinder _typeFinder;
        readonly Dictionary<string, Type> _artifactTypeToTypeMaps = new Dictionary<string, Type>();
        readonly Dictionary<Type, IArtifactType> _typeToArtifactTypeMaps = new Dictionary<Type, IArtifactType>();
        private readonly IContainer _container;


        /// <summary>
        /// Initializes a new instance of <see cref="ArtifactTypeToTypeMaps"/>
        /// </summary>
        /// <param name="typeFinder"><see cref="ITypeFinder"/> for discovering <see cref="IArtifactTypeMapFor{T}"/> types</param>
        /// <param name="container"><see cref="IContainer"/> for creating instances of the maps</param>
        public ArtifactTypeToTypeMaps(ITypeFinder typeFinder, IContainer container)
        {
            _typeFinder = typeFinder;
            _container = container;
            Populate();
        }



        /// <inheritdoc/>
        public IArtifactType Map(Type type)
        {
            ThrowIfMissingArtifactType(type);
            var candidates = _typeToArtifactTypeMaps.Keys.Where(t => t.IsAssignableFrom(type));

            Type underlyingType = null;
            if( candidates.Count() == 1 ) underlyingType = candidates.Single();
            else underlyingType = FindClosestCandidateFor(type, candidates);

            return _typeToArtifactTypeMaps[underlyingType];
        }

        /// <inheritdoc/>
        public Type Map(IArtifactType type)
        {
            ThrowIfMissingType(type);
            return _artifactTypeToTypeMaps[type.Identifier];
        }

        void BuildInheritanceTreeFor(Type type, List<Type> inherited)
        {
            var currentLevelInherited = new List<Type>();
            if( type.BaseType != null ) currentLevelInherited.Add(type.BaseType);
            currentLevelInherited.AddRange(type.GetInterfaces());
            inherited.AddRange(currentLevelInherited);
            currentLevelInherited.ForEach(t => BuildInheritanceTreeFor(t, inherited));
        }

        Type FindClosestCandidateFor(Type type, IEnumerable<Type> candidates)
        {
            Type underlyingType = null;
            var inheritanceList = new List<Type>();
            BuildInheritanceTreeFor(type, inheritanceList);

            var previousIndex = Int32.MaxValue;

            candidates.ForEach(candidate =>
            {
                var index = inheritanceList.IndexOf(candidate);
                if (index < previousIndex)
                {
                    underlyingType = candidate;
                    previousIndex = index;
                }
            });
            return underlyingType;
        }
        

        void Populate()
        {   
            var maps = _typeFinder.FindMultiple(typeof(IArtifactTypeMapFor<>));
            maps.ForEach(mapType => {
                var artifactTypeTargetType = mapType.GetInterface(typeof(IArtifactTypeMapFor<>).Name).GenericTypeArguments[0];
                var artifactType = _container.Get(mapType) as IArtifactType;

                _artifactTypeToTypeMaps[artifactType.Identifier] = artifactTypeTargetType;
                _typeToArtifactTypeMaps[artifactTypeTargetType] = artifactType;
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