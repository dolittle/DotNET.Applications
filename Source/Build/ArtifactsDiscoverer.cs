/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Dolittle.Logging;
using Dolittle.Reflection;

namespace Dolittle.Build
{
    /// <summary>
    /// Represents a class that can do discovery of Artifacts in an Assembly
    /// </summary>
    public class ArtifactsDiscoverer
    {
        readonly IAssemblyLoader _assemblyLoader;
        readonly ArtifactType[] _artifactTypes;
        readonly ILogger _logger;

        /// <summary>
        /// Gets the list of discovered Artifacts
        /// </summary>
        public Type[] Artifacts {get;}
        /// <summary>
        /// Instantiates and instance of <see cref="ArtifactsDiscoverer"/>
        /// </summary>
        /// <param name="assemblyLoader"></param>
        /// <param name="artifactTypes"></param>
        /// <param name="logger"></param>
        public ArtifactsDiscoverer(IAssemblyLoader assemblyLoader, DolittleArtifactTypes artifactTypes, ILogger logger)
        {
            _assemblyLoader = assemblyLoader;
            _artifactTypes = artifactTypes.ArtifactTypes;
            _logger = logger;

            Artifacts = DiscoverArtifacts();
        }

        Type[] DiscoverArtifacts()
        {
            
            var startTime = DateTime.UtcNow;
            var types = GetArtifactsFromAssembly();

            ThrowIfArtifactWithNoModuleOrFeature(types);
            
            return types;
        }
        Type[] GetArtifactsFromAssembly()
        {
            return _assemblyLoader
                .GetProjectReferencedAssemblies()
                .SelectMany(_ => _.ExportedTypes)
                .Where(_ =>
                    !_.GetTypeInfo().IsAbstract && !_.ContainsGenericParameters
                    && 
                    _artifactTypes
                    .Any(at => at.Type.IsAssignableFrom(_)))
                .ToArray();
        }

        void ThrowIfArtifactWithNoModuleOrFeature(Type[] types)
        {
            bool hasInvalidArtifact = false;
            foreach(var type in types)
            {
                if (string.IsNullOrEmpty(type.Namespace) || type.Namespace == "null") 
                {
                    _logger.Error($"Artifact {type.FullName} is invalid. Artifact has no namespace");
                    hasInvalidArtifact = true;      
                } 
                var numSegments = type.Namespace.Split(".").Count();
                if (numSegments < 1) 
                {
                    hasInvalidArtifact = true;
                    _logger.Error($"Artifact {type.Name} with namespace = {type.Namespace} is invalid. An artifact's namespace must consist of at least two segments.");
                }
            }
            if (hasInvalidArtifact) throw new InvalidArtifact();
        }
    }
}
