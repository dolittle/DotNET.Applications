// Copyright (c) Dolittle. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Dolittle.Applications;
using Dolittle.Artifacts;
using Dolittle.Artifacts.Configuration;
using Dolittle.Build.Topology;

namespace Dolittle.Build.Artifacts
{
    /// <summary>
    /// Extensions for <see cref="ArtifactsConfiguration"/> that's specific for the Build.Artifact namespace.
    /// </summary>
    public static class ArtifactsConfigurationExtensions
    {
        /// <summary>
        /// Returns all <see cref="ArtifactDefinition"/> instances in the <see cref="ArtifactsConfiguration"/>.
        /// </summary>
        /// <param name="configuration">The <see cref="ArtifactsConfiguration"/> being extended.</param>
        /// <returns>All maps of <see cref="ArtifactId"/> to <see cref="ArtifactDefinition"/>.</returns>
        public static IEnumerable<KeyValuePair<ArtifactId, ArtifactDefinition>> GetAllArtifactDefinitions(this ArtifactsConfiguration configuration)
        {
            var artifactDefinitions = new List<KeyValuePair<ArtifactId, ArtifactDefinition>>();

            foreach (var artifactEntry in configuration)
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
        /// <param name="configuration">The <see cref="ArtifactsConfiguration"/> being extended.</param>
        /// <param name="targetProperty">The target <see cref="PropertyInfo"/>.</param>
        /// <returns>All maps of <see cref="ArtifactId"/> to <see cref="ArtifactDefinition"/>.</returns>
        public static IEnumerable<KeyValuePair<ArtifactId, ArtifactDefinition>> GetAllArtifactDefinitions(this ArtifactsConfiguration configuration, PropertyInfo targetProperty)
        {
            var artifactDefinitions = new List<KeyValuePair<ArtifactId, ArtifactDefinition>>();

            foreach (var artifactEntry in configuration)
            {
                var selectedArtifacts = targetProperty.GetValue(artifactEntry.Value) as IDictionary<ArtifactId, ArtifactDefinition>;
                artifactDefinitions.AddRange(selectedArtifacts);
            }

            return artifactDefinitions;
        }

        /// <summary>
        /// Returns all <see cref="ArtifactDefinition"/> instances with a specific <see cref="Feature"/> (id) in the <see cref="ArtifactsConfiguration"/>.
        /// </summary>
        /// <param name="configuration">The <see cref="ArtifactsConfiguration"/> being extended.</param>
        /// <param name="feature">The <see cref="Feature"/> to get for.</param>
        /// <returns>All maps of <see cref="ArtifactId"/> to <see cref="ArtifactDefinition"/>.</returns>
        public static IEnumerable<KeyValuePair<ArtifactId, ArtifactDefinition>> GetAllArtifactDefinitions(this ArtifactsConfiguration configuration, Feature feature)
        {
            var artifactDefinitions = new List<KeyValuePair<ArtifactId, ArtifactDefinition>>();

            var artifact = configuration[feature];

            artifactDefinitions.AddRange(artifact.Commands);
            artifactDefinitions.AddRange(artifact.Events);
            artifactDefinitions.AddRange(artifact.EventSources);
            artifactDefinitions.AddRange(artifact.Queries);
            artifactDefinitions.AddRange(artifact.ReadModels);

            return artifactDefinitions;
        }

        /// <summary>
        /// Gets the <see cref="ArtifactId"/> that corresponds to the artifact's <see cref="Type"/>.
        /// </summary>
        /// <param name="configuration">The <see cref="ArtifactsConfiguration"/> being extended.</param>
        /// <param name="artifact">The <see cref="Type"/> representing the artifact to get for.</param>
        /// <returns>The <see cref="ArtifactId"/> for the <see cref="Type"/>.</returns>
        public static ArtifactId GetMatchingArtifactId(this ArtifactsConfiguration configuration, Type artifact)
        {
            var artifactDefinitions = configuration.GetAllArtifactDefinitions();

            return artifactDefinitions.Single(_ => _.Value.Type.GetActualType().Equals(artifact)).Key;
        }

        /// <summary>
        /// Gets a <see cref="ArtifactDefinition"/> that corresponds to the artifact's <see cref="Type"/>.
        /// </summary>
        /// <param name="configuration">The <see cref="ArtifactsConfiguration"/> being extended.</param>
        /// <param name="artifact">The <see cref="Type"/> representing the artifact to get for.</param>
        /// <returns>The <see cref="ArtifactDefinition"/> for the <see cref="Type"/>.</returns>
        public static ArtifactDefinition GetMatchingArtifactDefinition(this ArtifactsConfiguration configuration, Type artifact)
        {
            var artifactDefinitions = configuration.GetAllArtifactDefinitions();
            return artifactDefinitions.Single(_ => _.Value.Type.GetActualType().Equals(artifact)).Value;
        }

        /// <summary>
        /// Validates the <see cref="ArtifactsConfiguration"/> based on the bounded context's topology and the discoved artifact types in the assemblies of the bounded context.
        /// </summary>
        /// <param name="configuration">The <see cref="ArtifactsConfiguration"/> being extended.</param>
        /// <param name="boundedContextTopology"><see cref="BoundedContextTopology"/> to validate against.</param>
        /// <param name="types">All <see cref="Type">types</see> to validate.</param>
        /// <param name="buildMessages"><see cref="IBuildMessages"/> for outputting build messages.</param>
        public static void ValidateArtifacts(
            this ArtifactsConfiguration configuration,
            BoundedContextTopology boundedContextTopology,
            Type[] types,
            IBuildMessages buildMessages)
        {
            ThrowIfDuplicateArtifacts(configuration, buildMessages);
            WarnIfFeatureMissingFromTopology(configuration, boundedContextTopology, buildMessages);
            WarnIfArtifactNoLongerInStructure(configuration, types, buildMessages);
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
                {
                    idMap.Add(artifactDefinitionEntry.Key, artifactDefinitionEntry.Value.Type);
                }
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

                    foreach (var definitionEntry in artifacts.GetAllArtifactDefinitions(artifact.Key))
                        buildMessages.Warning($"\tArtifact: '{definitionEntry.Key.Value}' - '{definitionEntry.Value.Type.TypeString} @{definitionEntry.Value.Generation.Value}'");
                }
            }
        }

        static void WarnIfArtifactNoLongerInStructure(ArtifactsConfiguration artifacts, IEnumerable<Type> types, IBuildMessages messages)
        {
            var artifactDefinitions = new Dictionary<ArtifactId, ArtifactDefinition>();
            foreach (var artifactDefinitionEntry in artifacts.GetAllArtifactDefinitions())
            {
                if (!types.Contains(artifactDefinitionEntry.Value.Type.GetActualType()))
                    artifactDefinitions.Add(artifactDefinitionEntry.Key, artifactDefinitionEntry.Value);
            }

            if (artifactDefinitions.Count > 0)
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