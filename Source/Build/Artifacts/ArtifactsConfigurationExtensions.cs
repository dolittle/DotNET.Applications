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
using Dolittle.Collections;

namespace Dolittle.Build.Artifacts
{
    /// <summary>
    /// Extensions for <see cref="ArtifactsConfiguration"/> that's specific for the Build.Artifact namespace
    /// </summary>
    public static class ArtifactsConfigurationExtensions
    {
        /// <summary>
        /// Returns all <see cref="ArtifactDefinition"/> instances in the <see cref="ArtifactsConfiguration"/>
        /// </summary>
        public static IEnumerable<KeyValuePair<ArtifactId, ArtifactDefinition>> GetAllArtifactDefinitions(this ArtifactsConfiguration artifacts)
        {
            var artifactDefinitions = new List<KeyValuePair<ArtifactId,ArtifactDefinition>>();

            foreach (var artifactEntry in artifacts)
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
        /// Returns all <see cref="ArtifactDefinition"/> instances in the <see cref="ArtifactsConfiguration"/> by retrieving the <see cref="ArtifactDefinition"/> dictionaries with the <see cref="PropertyInfo"/>
        /// </summary>
        public static IEnumerable<KeyValuePair<ArtifactId, ArtifactDefinition>> GetAllArtifactDefinitions(this ArtifactsConfiguration artifacts, PropertyInfo targetProperty)
        {
            var artifactDefinitions = new List<KeyValuePair<ArtifactId,ArtifactDefinition>>();

            foreach (var artifactEntry in artifacts)
            {
                var selectedArtifacts = targetProperty.GetValue(artifactEntry.Value) as IDictionary<ArtifactId,ArtifactDefinition>;
                artifactDefinitions.AddRange(selectedArtifacts);
            }

            return artifactDefinitions;
        }
        /// <summary>
        /// Returns all <see cref="ArtifactDefinition"/> instances with a specific <see cref="Feature"/> (id) in the <see cref="ArtifactsConfiguration"/>
        /// </summary>
        public static IEnumerable<KeyValuePair<ArtifactId, ArtifactDefinition>> GetAllArtifactDefinitions(this ArtifactsConfiguration artifacts, Feature id)
        {
            var artifactDefinitions = new List<KeyValuePair<ArtifactId,ArtifactDefinition>>();

            var artifact = artifacts[id];

            artifactDefinitions.AddRange(artifact.Commands);
            artifactDefinitions.AddRange(artifact.Events);
            artifactDefinitions.AddRange(artifact.EventSources);
            artifactDefinitions.AddRange(artifact.Queries);
            artifactDefinitions.AddRange(artifact.ReadModels);
            
            return artifactDefinitions;
        }
        /// <summary>
        /// Gets the <see cref="ArtifactId"/> that corresponds to the artifact's <see cref="Type"/>
        /// </summary>
        public static ArtifactId GetMatchingArtifactId(this ArtifactsConfiguration artifacts, Type artifact)
        {
            var artifactDefinitions = artifacts.GetAllArtifactDefinitions();

            return artifactDefinitions.Single(_ => _.Value.Type.GetActualType().Equals(artifact)).Key;
        }
        /// <summary>
        /// Gets a <see cref="ArtifactDefinition"/> that corresponds to the artifact's <see cref="Type"/>
        /// </summary>
        public static ArtifactDefinition GetMatchingArtifactDefinition(this ArtifactsConfiguration artifacts, Type artifact)
        {
            var artifactDefinitions = artifacts.GetAllArtifactDefinitions();

            return artifactDefinitions.Single(_ => _.Value.Type.GetActualType().Equals(artifact)).Value;
        }
        /// <summary>
        /// Validates the <see cref="ArtifactsConfiguration"/> based on the bounded context's topology and the discoved artifact types in the assemblies of the bounded context
        /// </summary>
        public static void ValidateArtifacts(this ArtifactsConfiguration artifacts, BoundedContextTopology boundedContextTopology, Type[] types, IBuildMessages buildMessages)
        {
            ThrowIfDuplicateArtifacts(artifacts, buildMessages);
            WarnIfFeatureMissingFromTopology(artifacts, boundedContextTopology, buildMessages);
            WarnIfArtifactNoLongerInStructure(artifacts, types, buildMessages);
        }

        static void ThrowIfDuplicateArtifacts(ArtifactsConfiguration artifacts, IBuildMessages buildMessages)
        {
            var idMap = new Dictionary<ArtifactId, ClrType>();
            bool foundDuplicate = false;
            foreach (var artifactDefinitionEntry in artifacts.GetAllArtifactDefinitions())
            {
                if (idMap.ContainsKey(artifactDefinitionEntry.Key))
                {
                    foundDuplicate = true;
                    var artifactId = artifactDefinitionEntry.Key;
                    var clrType = idMap[artifactId];
                    
                    buildMessages.Error($"The artifacts '{clrType.TypeString}' and '{artifactDefinitionEntry.Value.Type.TypeString}' has the same ArtifactId: '{artifactId}'");
                    
                }
                else 
                    idMap.Add(artifactDefinitionEntry.Key, artifactDefinitionEntry.Value.Type);
            }
            if (foundDuplicate) throw new DuplicateArtifact();
        }
        static void WarnIfFeatureMissingFromTopology(ArtifactsConfiguration artifacts, BoundedContextTopology boundedContextTopology, IBuildMessages buildMessages)
        {
            Dictionary<Feature, FeatureName> featureMap = boundedContextTopology.RetrieveAllFeatureIds();

            foreach (var artifact in artifacts)
            {
                if (!featureMap.ContainsKey(artifact.Key))
                {
                    buildMessages.Warning($"Found artifacts under a Feature that does not exist in the topology. Feature: '{artifact.Key}':");
                    buildMessages.Warning("Artifacts:");
                    
                    var artifactDefinitions = artifacts.GetAllArtifactDefinitions(artifact.Key);
                    foreach (var definitionEntry in artifactDefinitions)
                        buildMessages.Warning($"\tArtifact: '{definitionEntry.Key.Value}' - '{definitionEntry.Value.Type.TypeString} @{definitionEntry.Value.Generation.Value}'");
                    
                }
            }
        }
        static void WarnIfArtifactNoLongerInStructure(ArtifactsConfiguration artifacts, IEnumerable<Type> types, IBuildMessages messages)
        {
            var artifactDefinitions = new Dictionary<ArtifactId,ArtifactDefinition>();
            foreach (var artifactDefinitionEntry in artifacts.GetAllArtifactDefinitions())
            {
                if (!types.Contains(artifactDefinitionEntry.Value.Type.GetActualType()))
                    artifactDefinitions.Add(artifactDefinitionEntry.Key, artifactDefinitionEntry.Value);
                
            }
            if (artifactDefinitions.Any())
            {
                messages.Warning("There are artifacts that are not found in the Bounded Context's artifacts file:");
                messages.Warning("Artifacts:");
                foreach (var artifactDefinitionEntry in artifactDefinitions)
                    messages.Warning($"\tArtifact: '{artifactDefinitionEntry.Key.Value}' - '{artifactDefinitionEntry.Value.Type.TypeString} @{artifactDefinitionEntry.Value.Generation.Value}'");
                throw new ArtifactNoLongerInStructure();
            }
        }

    }
}