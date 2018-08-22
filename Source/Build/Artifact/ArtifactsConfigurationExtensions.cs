/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using System;
using System.Collections.Generic;
using System.Linq;
using Dolittle.Applications;
using Dolittle.Applications.Configuration;
using Dolittle.Artifacts;
using Dolittle.Artifacts.Configuration;
using Dolittle.Logging;

namespace Dolittle.Build.Artifact
{
    /// <summary>
    /// Extensions for <see cref="ArtifactsConfiguration"/> that's specific for the Build.Artifact namespace
    /// </summary>
    public static class ArtifactsConfigurationExtensions
    {
        /// <summary>
        /// Returns all <see cref="ArtifactDefinition"/> instances in the <see cref="ArtifactsConfiguration"/>
        /// </summary>
        public static IEnumerable<ArtifactDefinition> GetAllArtifactDefinitions(this ArtifactsConfiguration configuration)
        {
            var artifactDefinitions = new List<ArtifactDefinition>();

            foreach (var artifactEntry in configuration.Artifacts)
            {
                artifactDefinitions.AddRange(artifactEntry.Value.Commands);
                artifactDefinitions.AddRange(artifactEntry.Value.EventProcessors);
                artifactDefinitions.AddRange(artifactEntry.Value.Events);
                artifactDefinitions.AddRange(artifactEntry.Value.EventSources);
                artifactDefinitions.AddRange(artifactEntry.Value.Queries);
                artifactDefinitions.AddRange(artifactEntry.Value.ReadModels);
                
            }
            return artifactDefinitions;
        }
        /// <summary>
        /// Returns all <see cref="ArtifactDefinition"/> instances with a specific <see cref="Feature"/> (id) in the <see cref="ArtifactsConfiguration"/>
        /// </summary>
        public static IEnumerable<ArtifactDefinition> GetAllArtifactDefinitions(this ArtifactsConfiguration configuration, Feature id)
        {
            var artifactDefinitions = new List<ArtifactDefinition>();

            var artifacts = configuration.Artifacts[id];
            
            artifactDefinitions.AddRange(artifacts.Commands);
            artifactDefinitions.AddRange(artifacts.EventProcessors);
            artifactDefinitions.AddRange(artifacts.Events);
            artifactDefinitions.AddRange(artifacts.EventSources);
            artifactDefinitions.AddRange(artifacts.Queries);
            artifactDefinitions.AddRange(artifacts.ReadModels);
            
            return artifactDefinitions;
        }
        /// <summary>
        /// Validates the <see cref="ArtifactsConfiguration"/> based on the bounded context's topology and the discoved artifact types in the assemblies of the bounded context
        /// </summary>
        public static void ValidateArtifacts(this ArtifactsConfiguration artifactsConfiguration, BoundedContextConfiguration boundedContextConfiguration, Type[] types, ILogger logger)
        {
            ThrowIfDuplicateArtifacts(artifactsConfiguration, logger);
            WarnIfFeatureMissingFromTopology(artifactsConfiguration, boundedContextConfiguration, logger);
            WarnIfArtifactNoLongerInStructure(artifactsConfiguration, types, logger);
        }

        static void ThrowIfDuplicateArtifacts(ArtifactsConfiguration artifactsConfiguration, ILogger logger)
        {
            var idMap = new Dictionary<ArtifactId, ClrType>();
            bool foundDuplicate = false;
            foreach (var artifactDefinition in artifactsConfiguration.GetAllArtifactDefinitions())
            {
                if (idMap.ContainsKey(artifactDefinition.Artifact))
                {
                    foundDuplicate = true;
                    var artifactId = artifactDefinition.Artifact;
                    var clrType = idMap[artifactDefinition.Artifact];
                    
                    logger.Error($"An artifact with ArtifactId: {artifactId.Value} already exists.");
                    logger.Debug($"Artifact with Type: {clrType.TypeString} and artifact with Type {artifactDefinition.Type.TypeString} has the same ArtifactId.");
                }
                else 
                    idMap.Add(artifactDefinition.Artifact, artifactDefinition.Type);
            }
            if (foundDuplicate) throw new DuplicateArtifact();
        }
        static void WarnIfFeatureMissingFromTopology(ArtifactsConfiguration artifactsConfiguration, BoundedContextConfiguration boundedContextConfiguration, ILogger logger)
        {
            Dictionary<Feature, FeatureName> featureMap = boundedContextConfiguration.RetrieveAllFeatureIds();

            foreach (var artifact in artifactsConfiguration.Artifacts)
            {
                if (!featureMap.ContainsKey(artifact.Key))
                {
                    logger.Warning($"Found artifacts under a Feature that does not exist in the topology. Feature: {artifact.Key}");
                    logger.Debug("Artifacts:");
                    
                    var artifactDefinitions = artifactsConfiguration.GetAllArtifactDefinitions(artifact.Key);
                    foreach (var definition in artifactDefinitions)
                        logger.Debug($"\tArtifact: {definition.Artifact.Value} CLR-type: {definition.Type.TypeString} @{definition.Generation.Value}");
                    
                }
            }
        }
        static void WarnIfArtifactNoLongerInStructure(ArtifactsConfiguration artifactsConfiguration, IEnumerable<Type> types, ILogger logger)
        {
            var artifactDefinitions = new List<ArtifactDefinition>();
            foreach (var artifactDefinition in artifactsConfiguration.GetAllArtifactDefinitions())
            {
                if (!types.Contains(artifactDefinition.Type.GetActualType()))
                    artifactDefinitions.Add(artifactDefinition);
                
            }
            if (artifactDefinitions.Any())
            {
                logger.Warning("There are artifacts that are not found in the Bounded Context structure anymore. You may have to write migrators for them.");
                logger.Debug("Artifacts:");
                foreach (var artifactDefinition in artifactDefinitions)
                    logger.Debug($"\tArtifact: {artifactDefinition.Artifact.Value} CLR-type: {artifactDefinition.Type.TypeString} @{artifactDefinition.Generation.Value}");
            }
        }

    }
}