// Copyright (c) Dolittle. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Dolittle.ApplicationModel;
using Dolittle.Artifacts;
using Dolittle.Artifacts.Configuration;
using Dolittle.Build.Topology;
using Dolittle.Reflection;
using MutableArtifactsByTypeDictionary = System.Collections.Generic.Dictionary<System.Reflection.PropertyInfo, System.Collections.Generic.Dictionary<Dolittle.Artifacts.ArtifactId, Dolittle.Artifacts.Configuration.ArtifactDefinition>>;
using MutableArtifactsDictionary = System.Collections.Generic.Dictionary<Dolittle.ApplicationModel.Feature, System.Collections.Generic.Dictionary<System.Reflection.PropertyInfo, System.Collections.Generic.Dictionary<Dolittle.Artifacts.ArtifactId, Dolittle.Artifacts.Configuration.ArtifactDefinition>>>;

namespace Dolittle.Build.Artifacts
{
    /// <summary>
    /// Represents a class that can build a valid <see cref="ArtifactsConfiguration"/>.
    /// </summary>
    public class ArtifactsConfigurationBuilder
    {
        readonly IEnumerable<Type> _artifacts;
        readonly IBuildMessages _buildMessages;
        readonly ArtifactTypes _artifactTypes;
        readonly ArtifactsConfiguration _currentArtifactsConfiguration;

        /// <summary>
        /// Initializes a new instance of the <see cref="ArtifactsConfigurationBuilder"/> class.
        /// </summary>
        /// <param name="artifacts">The discovered types of artifacts in the Bounded Context's assemblies.</param>
        /// <param name="currentArtifactsConfiguration">The current <see cref="ArtifactsConfiguration"/> that will be used as a base for building a valid updated configuration that is returned from Build.</param>
        /// <param name="artifactTypes">A list of <see cref="ArtifactType"/> which represents the different artifact types.</param>
        /// <param name="buildMessages">The <see cref="IBuildMessages"/> for outputting build messages.</param>
        public ArtifactsConfigurationBuilder(
            IEnumerable<Type> artifacts,
            ArtifactsConfiguration currentArtifactsConfiguration,
            ArtifactTypes artifactTypes,
            IBuildMessages buildMessages)
        {
            _artifacts = artifacts;
            _buildMessages = buildMessages;

            _artifactTypes = artifactTypes;
            _currentArtifactsConfiguration = currentArtifactsConfiguration;
        }

        /// <summary>
        /// Builds a valid <see cref="ArtifactsConfiguration"/> based on a <see cref="MicroserviceTopology"/> .
        /// </summary>
        /// <param name="microserviceTopology">The <see cref="MicroserviceTopology"/>.</param>
        /// <returns>The built <see cref="ArtifactsConfiguration"/>.</returns>
        public ArtifactsConfiguration Build(MicroserviceTopology microserviceTopology)
        {
            var newArtifacts = 0;

            var artifactsDictionary = new MutableArtifactsDictionary();

            foreach (var (feature, featureArtifactsByType) in _currentArtifactsConfiguration)
            {
                var featureArtifacts = artifactsDictionary[feature] = new MutableArtifactsByTypeDictionary();
                foreach (var artifactType in featureArtifactsByType.GetType().GetProperties())
                {
                    var existingArtifactsForFeatureType = artifactType.GetValue(featureArtifactsByType) as IReadOnlyDictionary<ArtifactId, ArtifactDefinition>;
                    featureArtifacts[artifactType] = new Dictionary<ArtifactId, ArtifactDefinition>(existingArtifactsForFeatureType.ToDictionary(_ => _.Key, _ => _.Value));
                }
            }

            var nonMatchingArtifacts = new List<string>();
            foreach (var artifactType in _artifactTypes)
            {
                newArtifacts += HandleArtifactOfType(
                    artifactType,
                    microserviceTopology,
                    artifactsDictionary,
                    nonMatchingArtifacts);
            }

            if (nonMatchingArtifacts.Count > 0)
            {
                foreach (var artifactNamespace in nonMatchingArtifacts)
                    _buildMessages.Warning($"An artifact with namespace: '{artifactNamespace}' could not be matched with any feature in the Bounded Context's topology");

                throw new NonMatchingArtifact();
            }

            var artifactsByTypeDefinitionConstructor = typeof(ArtifactsByTypeDefinition).GetConstructors().Single(_ => _.GetParameters().All(p => p.ParameterType.Equals(typeof(IDictionary<ArtifactId, ArtifactDefinition>))));

            var updatedArtifactsConfiguration = new ArtifactsConfiguration(new Dictionary<Feature, ArtifactsByTypeDefinition>(
                artifactsDictionary.Select(_ =>
                {
                    var feature = _.Key;
                    var arguments = artifactsByTypeDefinitionConstructor.GetParameters().Select(arg => _.Value.SingleOrDefault(prop => arg.Name.Equals(prop.Key.Name, StringComparison.InvariantCultureIgnoreCase)).Value ?? new Dictionary<ArtifactId, ArtifactDefinition>()).ToArray();
                    var artifacts = artifactsByTypeDefinitionConstructor.Invoke(arguments) as ArtifactsByTypeDefinition;
                    return new KeyValuePair<Feature, ArtifactsByTypeDefinition>(feature, artifacts);
                }).ToDictionary(_ => _.Key, _ => _.Value)));
            updatedArtifactsConfiguration.ValidateArtifacts(microserviceTopology, _artifacts, _buildMessages);

            if (newArtifacts > 0)
            {
                _buildMessages.Information($"Added {newArtifacts} new artifacts to the map.");
            }
            else
            {
                _buildMessages.Information("No new artifacts added to the map.");
            }

            return updatedArtifactsConfiguration;
        }

        int HandleArtifactOfType(ArtifactType artifactType, MicroserviceTopology microserviceConfiguration, MutableArtifactsDictionary artifactsDictionary, List<string> nonMatchingArtifacts)
        {
            var targetProperty = artifactType.TargetPropertyExpression.GetPropertyInfo();

            var newArtifacts = 0;
            foreach (var artifact in _artifacts.Where(_ => artifactType.Type.IsAssignableFrom(_)))
            {
                var feature = microserviceConfiguration.FindMatchingFeature(artifact.Namespace, nonMatchingArtifacts);
                if (feature.Value != null)
                {
                    if (!artifactsDictionary.TryGetValue(feature.Key, out MutableArtifactsByTypeDictionary artifactsByType))
                        artifactsByType = artifactsDictionary[feature.Key] = new Dictionary<PropertyInfo, Dictionary<ArtifactId, ArtifactDefinition>>();

                    if (!artifactsByType.TryGetValue(targetProperty, out Dictionary<ArtifactId, ArtifactDefinition> mutableArtifacts))
                        mutableArtifacts = artifactsByType[targetProperty] = new Dictionary<ArtifactId, ArtifactDefinition>();

                    if (!mutableArtifacts.Any(_ => _.Value.Type.GetActualType() == artifact))
                    {
                        var artifactObject = new Artifact(ArtifactId.New(), ArtifactGeneration.First);
                        if (artifact.HasAttribute<ArtifactAttribute>())
                            artifactObject = (artifact.GetTypeInfo().GetCustomAttributes(typeof(ArtifactAttribute), false).First() as ArtifactAttribute)?.Artifact;

                        AddNewArtifact(artifactObject, artifact, mutableArtifacts, artifactType.TypeName);
                        newArtifacts++;
                    }
                    else
                    {
                        if (artifact.HasAttribute<ArtifactAttribute>())
                        {
                            var artifactObject = (artifact.GetTypeInfo().GetCustomAttributes(typeof(ArtifactAttribute), false).First() as ArtifactAttribute)?.Artifact;

                            var existingArtifact = mutableArtifacts.Single(_ => _.Value.Type.GetActualType() == artifact);
                            if (!existingArtifact.Key.Value.Equals(artifactObject.Id.Value))
                            {
                                mutableArtifacts.Remove(existingArtifact.Key);
                                AddNewArtifact(artifactObject, artifact, mutableArtifacts, artifactType.TypeName);
                                newArtifacts++;
                            }
                        }
                    }
                }
            }

            return newArtifacts;
        }

        void AddNewArtifact(Artifact artifactObject, Type artifact, IDictionary<ArtifactId, ArtifactDefinition> mutableArtifacts, string artifactTypeName)
        {
            var artifactDefinition = new ArtifactDefinition(artifactObject.Generation, ClrType.FromType(artifact));
            _buildMessages.Trace($"Adding '{artifact.Name}' as a new {artifactTypeName} artifact with identifier '{artifactObject.Id}'");
            mutableArtifacts[artifactObject.Id] = artifactDefinition;
        }
    }
}