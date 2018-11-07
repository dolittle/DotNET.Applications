/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Dolittle.Applications;
using Dolittle.Applications.Configuration;
using Dolittle.Artifacts;
using Dolittle.Artifacts.Configuration;
using Dolittle.Build.Topology;

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
                artifactDefinitions.AddRange(artifactEntry.Value.Events);
                artifactDefinitions.AddRange(artifactEntry.Value.EventSources);
                artifactDefinitions.AddRange(artifactEntry.Value.Queries);
                artifactDefinitions.AddRange(artifactEntry.Value.ReadModels);
                
            }
            return artifactDefinitions;
        }
        /// <summary>
        /// Returns all <see cref="ArtifactDefinition"/> instances in the <see cref="ArtifactsConfiguration"/> by retrieving the <see cref="ArtifactDefinition"/> lists with the <see cref="PropertyInfo"/>
        /// </summary>
        public static IEnumerable<ArtifactDefinition> GetAllArtifactDefinitions(this ArtifactsConfiguration configuration, PropertyInfo targetProperty)
        {
            var artifactDefinitions = new List<ArtifactDefinition>();

            foreach (var artifactEntry in configuration.Artifacts)
                artifactDefinitions.AddRange(targetProperty.GetValue(artifactEntry.Value) as IEnumerable<ArtifactDefinition>);

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
            artifactDefinitions.AddRange(artifacts.Events);
            artifactDefinitions.AddRange(artifacts.EventSources);
            artifactDefinitions.AddRange(artifacts.Queries);
            artifactDefinitions.AddRange(artifacts.ReadModels);
            
            return artifactDefinitions;
        }
        /// <summary>
        /// Gets a <see cref="ArtifactDefinition"/> that corresponds to the artifact's <see cref="Type"/>
        /// </summary>
        public static ArtifactDefinition GetMatchingArtifactDefinition(this ArtifactsConfiguration artifactsConfiguration, Type artifact)
        {
            var artifactDefinitions = artifactsConfiguration.GetAllArtifactDefinitions();

            return artifactDefinitions.Single(_ => _.Type.GetActualType().Equals(artifact));
        }
        /// <summary>
        /// Validates the <see cref="ArtifactsConfiguration"/> based on the bounded context's topology and the discoved artifact types in the assemblies of the bounded context
        /// </summary>
        public static void ValidateArtifacts(this ArtifactsConfiguration artifactsConfiguration, BoundedContextTopology boundedContextTopology, Type[] types, IBuildToolLogger logger)
        {
            ThrowIfDuplicateArtifacts(artifactsConfiguration, logger);
            WarnIfFeatureMissingFromTopology(artifactsConfiguration, boundedContextTopology, logger);
            WarnIfArtifactNoLongerInStructure(artifactsConfiguration, types, logger);
        }

        static void ThrowIfDuplicateArtifacts(ArtifactsConfiguration artifactsConfiguration, IBuildToolLogger logger)
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
                    
                    logger.Error($"The artifacts '{clrType.TypeString}' and '{artifactDefinition.Type.TypeString}' has the same ArtifactId: '{artifactId}'");
                    
                }
                else 
                    idMap.Add(artifactDefinition.Artifact, artifactDefinition.Type);
            }
            if (foundDuplicate) throw new DuplicateArtifact();
        }
        static void WarnIfFeatureMissingFromTopology(ArtifactsConfiguration artifactsConfiguration, BoundedContextTopology boundedContextTopology, IBuildToolLogger logger)
        {
            Dictionary<Feature, FeatureName> featureMap = boundedContextTopology.RetrieveAllFeatureIds();

            foreach (var artifact in artifactsConfiguration.Artifacts)
            {
                if (!featureMap.ContainsKey(artifact.Key))
                {
                    logger.Warning($"Found artifacts under a Feature that does not exist in the topology. Feature: '{artifact.Key}':");
                    logger.Warning("Artifacts:");
                    
                    var artifactDefinitions = artifactsConfiguration.GetAllArtifactDefinitions(artifact.Key);
                    foreach (var definition in artifactDefinitions)
                        logger.Warning($"\tArtifact: '{definition.Artifact.Value}' - '{definition.Type.TypeString} @{definition.Generation.Value}'");
                    
                }
            }
        }
        static void WarnIfArtifactNoLongerInStructure(ArtifactsConfiguration artifactsConfiguration, IEnumerable<Type> types, IBuildToolLogger logger)
        {
            var artifactDefinitions = new List<ArtifactDefinition>();
            foreach (var artifactDefinition in artifactsConfiguration.GetAllArtifactDefinitions())
            {
                if (!types.Contains(artifactDefinition.Type.GetActualType()))
                    artifactDefinitions.Add(artifactDefinition);
                
            }
            if (artifactDefinitions.Any())
            {
                logger.Warning("There are artifacts that are not found in the Bounded Context's artifacts file:");
                logger.Warning("Artifacts:");
                foreach (var artifactDefinition in artifactDefinitions)
                    logger.Warning($"\tArtifact: '{artifactDefinition.Artifact.Value}' - '{artifactDefinition.Type.TypeString} @{artifactDefinition.Generation.Value}'");
                throw new ArtifactNoLongerInStructure();
            }
        }

    }
}