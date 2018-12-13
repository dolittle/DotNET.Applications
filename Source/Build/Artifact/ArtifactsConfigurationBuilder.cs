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
            
            var artifactsDictionary = new Dictionary<Feature, ArtifactsByTypeDefinition>(_currentArtifactsConfiguration);
            var nonMatchingArtifacts = new List<string>();
            foreach (var artifactType in _artifactTypes.ArtifactTypes) 
            {
                newArtifacts += HandleArtifactOfType(
                    artifactType.Type,
                    boundedContextTopology,
                    artifactType.TypeName,
                    artifactType.TargetPropertyExpression,
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

            var updatedArtifactsConfiguration = new ArtifactsConfiguration(artifactsDictionary);
            updatedArtifactsConfiguration.ValidateArtifacts(boundedContextTopology, _artifacts, _logger);

            if (newArtifacts > 0)
            {
                _logger.Information($"Added {newArtifacts} new artifacts to the map.");
            }
            else 
                _logger.Information($"No new artifacts added to the map.");
            
            return updatedArtifactsConfiguration;
        }

        int HandleArtifactOfType(Type artifactType, BoundedContextTopology boundedContextConfiguration, string artifactTypeName, Expression<Func<ArtifactsByTypeDefinition, IEnumerable<ArtifactDefinition>> > targetPropertyExpression, Dictionary<Feature, ArtifactsByTypeDefinition> artifactsDictionary, List<string> nonMatchingArtifacts)
        {
            var targetProperty = targetPropertyExpression.GetPropertyInfo();

            var newArtifacts = 0;
            var artifacts = _artifacts.Where(_ => artifactType.IsAssignableFrom(_));
            
            foreach (var artifact in artifacts)
            {
                var feature = boundedContextConfiguration.FindMatchingFeature(artifact.Namespace, ref nonMatchingArtifacts);
                if (feature != null)
                {
                    ArtifactsByTypeDefinition artifactsByTypeDefinition;

                    if (artifactsDictionary.ContainsKey(feature.Feature))
                        artifactsByTypeDefinition = artifactsDictionary[feature.Feature];
                    else
                    {
                        artifactsByTypeDefinition = new ArtifactsByTypeDefinition();
                        artifactsDictionary[feature.Feature] = artifactsByTypeDefinition;
                    } 
                    var existingArtifacts = targetProperty.GetValue(artifactsByTypeDefinition) as IEnumerable<ArtifactDefinition>;
                    
                    if (!existingArtifacts.Any(_ => _.Type.GetActualType() == artifact))
                    {
                        var artifactObject = new Dolittle.Artifacts.Artifact(ArtifactId.New(), ArtifactGeneration.First);
                        if (artifact.HasAttribute<ArtifactAttribute>())
                            artifactObject = (artifact.GetTypeInfo().GetCustomAttributes(typeof(ArtifactAttribute), false).First() as ArtifactAttribute).Artifact;
                        
                        SetNewAndExistingArtifacts(artifactObject, artifact, artifactsByTypeDefinition, targetProperty, existingArtifacts, artifactTypeName);
                        newArtifacts++;
                    }
                    else
                    {
                        if (artifact.HasAttribute<ArtifactAttribute>())
                        {
                            var artifactObject = (artifact.GetTypeInfo().GetCustomAttributes(typeof(ArtifactAttribute), false).First() as ArtifactAttribute).Artifact;
                            
                            var existingArtifact = existingArtifacts.Single(_ => _.Type.GetActualType() == artifact);
                            if (! existingArtifact.Artifact.Value.Equals(artifactObject.Id.Value))
                            {
                                existingArtifacts = existingArtifacts.Where(_ => _.Artifact.Value != existingArtifact.Artifact.Value);
                                SetNewAndExistingArtifacts(artifactObject, artifact, artifactsByTypeDefinition, targetProperty, existingArtifacts, artifactTypeName);
                                newArtifacts++;
                            }
                        }
                    }
                }
            }
            return newArtifacts;
        }

        void SetNewAndExistingArtifacts(Artifacts.Artifact artifactObject, Type artifact, ArtifactsByTypeDefinition artifactsByTypeDefinition, PropertyInfo targetProperty, IEnumerable<ArtifactDefinition> existingArtifacts, string artifactTypeName)
        {
            var newAndExistingArtifacts = new List<ArtifactDefinition>(existingArtifacts);
            var artifactDefinition = new ArtifactDefinition
            {
                Artifact = artifactObject.Id,
                Generation = artifactObject.Generation,
                Type = ClrType.FromType(artifact)
            };
            _logger.Trace($"Adding '{artifact.Name}' as a new {artifactTypeName} artifact with identifier '{artifactDefinition.Artifact}'");
            newAndExistingArtifacts.Add(artifactDefinition);

            targetProperty.SetValue(artifactsByTypeDefinition, newAndExistingArtifacts);
        }
    }
}