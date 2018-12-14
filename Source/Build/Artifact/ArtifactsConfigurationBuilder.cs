/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using Dolittle.Applications;
using Dolittle.Applications.Configuration;
using Dolittle.Artifacts;
using Dolittle.Artifacts.Configuration;
using Dolittle.Build.Topology;
using Dolittle.Collections;
using Dolittle.Reflection;
using Dolittle.Serialization.Json;

namespace Dolittle.Build.Artifact
{
    using MutableArtifactsByTypeDictionary = Dictionary<PropertyInfo, Dictionary<ArtifactId, ArtifactDefinition>>;
    using MutableAritfactsDictionary = Dictionary<Feature, Dictionary<PropertyInfo, Dictionary<ArtifactId, ArtifactDefinition>>>;

    /// <summary>
    /// Represents a class that can build a valid <see cref="ArtifactsConfiguration"/>
    /// </summary>     
    public class ArtifactsConfigurationBuilder
    {
        readonly Type[] _artifacts;
        readonly IBuildToolLogger _logger;
        readonly DolittleArtifactTypes _artifactTypes;
        readonly ArtifactsConfiguration _currentArtifactsConfiguration;

        /// <summary>
        /// Instantiates an instance of <see cref="ArtifactsConfigurationBuilder"/>
        /// </summary>
        /// <param name="artifacts">The discovered types of artifacts in the Bounded Context's assemblies</param>
        /// <param name="currentArtifactsConfiguration">The current <see cref="ArtifactsConfiguration"/> that will be used as a base for building a valid updated configuration that is returned from Build</param>
        /// <param name="artifactTypes">A list of <see cref="ArtifactType"/> which represents the different artifact types</param>
        /// <param name="logger"></param>
        public ArtifactsConfigurationBuilder(Type[] artifacts, ArtifactsConfiguration currentArtifactsConfiguration, DolittleArtifactTypes artifactTypes, IBuildToolLogger logger)
        {
            _artifacts = artifacts;
            _logger = logger;

            _artifactTypes = artifactTypes;
            _currentArtifactsConfiguration = currentArtifactsConfiguration;
        }

        /// <summary>
        /// Builds a valid <see cref="ArtifactsConfiguration"/> based on a <see cref="BoundedContextTopology"/> 
        /// </summary>
        /// <param name="boundedContextTopology"></param>
        /// <returns></returns>
        public ArtifactsConfiguration Build(BoundedContextTopology boundedContextTopology)
        {
            var newArtifacts = 0;
            
            var artifactsDictionary = new MutableAritfactsDictionary();
            foreach (var (feature, featureArtifactsByType) in _currentArtifactsConfiguration)
            {
                var featureArtifacts = artifactsDictionary[feature] = new Dictionary<PropertyInfo, Dictionary<ArtifactId, ArtifactDefinition>>();
                foreach (var artifactType in featureArtifactsByType.GetType().GetProperties())
                {
                    var existingArtifactsForFeatureType = artifactType.GetValue(featureArtifactsByType) as IReadOnlyDictionary<ArtifactId, ArtifactDefinition>;
                    featureArtifacts[artifactType] = new Dictionary<ArtifactId, ArtifactDefinition>(existingArtifactsForFeatureType);
                }
            }

            var nonMatchingArtifacts = new List<string>();
            foreach (var artifactType in _artifactTypes.ArtifactTypes) 
            {
                newArtifacts += HandleArtifactOfType(
                    artifactType,
                    boundedContextTopology,
                    artifactsDictionary,
                    nonMatchingArtifacts
                );
            }
            if (nonMatchingArtifacts.Any())
            {
                foreach (var artifactNamespace in nonMatchingArtifacts)
                    _logger.Warning($"An artifact with namespace: '{artifactNamespace}' could not be matched with any feature in the Bounded Context's topology");
                
                throw new NonMatchingArtifact();
            }

            //new Dictionary<Feature, ArtifactsByTypeDefinition>()
            var artifactsByTypeDefinitionConstructor = typeof(ArtifactsByTypeDefinition).GetConstructors().Single(_ => _.GetParameters().All(p => p.ParameterType.Equals(typeof(IDictionary<ArtifactId, ArtifactDefinition>))));

            var updatedArtifactsConfiguration = new ArtifactsConfiguration(new Dictionary<Feature, ArtifactsByTypeDefinition>(
                artifactsDictionary.Select(_ => {
                    var feature = _.Key;
                    var arguments = artifactsByTypeDefinitionConstructor.GetParameters().Select(arg => {
                        return _.Value.SingleOrDefault(prop => arg.ParameterType.IsAssignableFrom(prop.Key.PropertyType) && arg.Name.Equals(prop.Key.Name, StringComparison.OrdinalIgnoreCase)).Value ?? new Dictionary<ArtifactId, ArtifactDefinition>();
                    }).ToArray();
                    var artifacts = artifactsByTypeDefinitionConstructor.Invoke(arguments) as ArtifactsByTypeDefinition;
                    return new KeyValuePair<Feature, ArtifactsByTypeDefinition>(feature, artifacts);
                })
            ));
            updatedArtifactsConfiguration.ValidateArtifacts(boundedContextTopology, _artifacts, _logger);

            if (newArtifacts > 0)
            {
                _logger.Information($"Added {newArtifacts} new artifacts to the map.");
            }
            else 
                _logger.Information($"No new artifacts added to the map.");
            
            return updatedArtifactsConfiguration;
        }

        int HandleArtifactOfType(ArtifactType artifactType, BoundedContextTopology boundedContextConfiguration, MutableAritfactsDictionary artifactsDictionary, List<string> nonMatchingArtifacts)
        {
            var targetProperty = artifactType.TargetPropertyExpression.GetPropertyInfo();

            var newArtifacts = 0;
            var artifacts = _artifacts.Where(_ => artifactType.Type.IsAssignableFrom(_));
            
            foreach (var artifact in artifacts)
            {
                var feature = boundedContextConfiguration.FindMatchingFeature(artifact.Namespace, nonMatchingArtifacts);
                if (feature != null)
                {
                    MutableArtifactsByTypeDictionary artifactsByType;
                    if (!artifactsDictionary.TryGetValue(feature.Feature, out artifactsByType))
                        artifactsByType = artifactsDictionary[feature.Feature] = new Dictionary<PropertyInfo, Dictionary<ArtifactId, ArtifactDefinition>>();

                    Dictionary<ArtifactId, ArtifactDefinition> mutableArtifacts;
                    if (artifactsByType.TryGetValue(targetProperty, out mutableArtifacts))
                        mutableArtifacts = artifactsByType[targetProperty] = new Dictionary<ArtifactId, ArtifactDefinition>();
                    
                    if (!mutableArtifacts.Any(_ => _.Value.Type.GetActualType() == artifact))
                    {
                        var artifactObject = new Dolittle.Artifacts.Artifact(ArtifactId.New(), ArtifactGeneration.First);
                        if (artifact.HasAttribute<ArtifactAttribute>())
                            artifactObject = (artifact.GetTypeInfo().GetCustomAttributes(typeof(ArtifactAttribute), false).First() as ArtifactAttribute).Artifact;
                        
                        AddNewArtifact(artifactObject, artifact, mutableArtifacts, artifactType.TypeName);
                        newArtifacts++;
                    }
                    else
                    {
                        if (artifact.HasAttribute<ArtifactAttribute>())
                        {
                            var artifactObject = (artifact.GetTypeInfo().GetCustomAttributes(typeof(ArtifactAttribute), false).First() as ArtifactAttribute).Artifact;
                            
                            var existingArtifact = mutableArtifacts.Single(_ => _.Value.Type.GetActualType() == artifact);
                            if (! existingArtifact.Key.Value.Equals(artifactObject.Id.Value))
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

        void AddNewArtifact(Artifacts.Artifact artifactObject, Type artifact, IDictionary<ArtifactId, ArtifactDefinition> mutableArtifacts, string artifactTypeName)
        {
            var artifactDefinition = new ArtifactDefinition(artifactObject.Generation, ClrType.FromType(artifact));
            _logger.Trace($"Adding '{artifact.Name}' as a new {artifactTypeName} artifact with identifier '{artifactObject.Id}'");
            mutableArtifacts[artifactObject.Id] = artifactDefinition;
        }
    }
}