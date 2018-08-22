using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Dolittle.Logging;

namespace Dolittle.Build
{
    /// <summary>
    /// Represents a class that can do discovery of Artifacts in an Assembly
    /// </summary>
    public class ArtifactsDiscoverer
    {
        readonly AssemblyLoader _assemblyLoader;
        readonly ArtifactType[] _artifactTypes;
        readonly ILogger _logger;

        /// <summary>
        /// Gets the list of discovered Artifacts
        /// </summary>
        public Type[] Artifacts {get;}
        /// <summary>
        /// Instantiates and instance of <see cref="ArtifactsDiscoverer"/>
        /// </summary>
        /// <param name="assemblyPath"></param>
        /// <param name="artifactTypes"></param>
        /// <param name="logger"></param>
        public ArtifactsDiscoverer(string assemblyPath, ArtifactType[] artifactTypes, ILogger logger)
        {
            _assemblyLoader = new AssemblyLoader(assemblyPath);
            _artifactTypes = artifactTypes;
            _logger = logger;

            Artifacts = DiscoverArtifacts();
        }

        Type[] DiscoverArtifacts()
        {
            _logger.Information("Discovering Artifacts");
            
            var startTime = DateTime.UtcNow;
            var types = GetArtifactsFromAssembly();

            ThrowIfArtifactWithNoModuleOrFeature(types);
            
            var endTime = DateTime.UtcNow;
            var deltaTime = endTime.Subtract(startTime);
            _logger.Information($"Finished artifact discovery process. (Took {deltaTime.TotalSeconds} seconds)");
            
            return types;
        }
        Type[] GetArtifactsFromAssembly()
        {
            return _assemblyLoader
                .GetProjectReferencedAssemblies()
                .SelectMany(_ => _.ExportedTypes)
                .Where(_ =>
                    _artifactTypes
                    .Any(at => at.Type.IsAssignableFrom(_)))
                .ToArray();
        }

        void ThrowIfArtifactWithNoModuleOrFeature(Type[] types)
        {
            bool hasInvalidArtifact = false;
            foreach(var type in types)
            {
                var numSegments = type.Namespace.Split(".").Count();
                if (numSegments < 1) 
                {
                    hasInvalidArtifact = true;
                    _logger.Error($"Artifact {type.Name} with namespace = {type.Namespace} is invalid");
                }
            }
            if (hasInvalidArtifact) throw new InvalidArtifact();
        }
    }
}