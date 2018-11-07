/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
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
        ArtifactsConfiguration _artifactsConfiguration;

        /// <summary>
        /// Instantiates an instance of <see cref="ArtifactsConfigurationBuilder"/>
        /// </summary>
        /// <param name="artifacts">The discovered types of artifacts in the Bounded Context's assemblies</param>
        /// <param name="artifactsConfiguration">The <see cref="ArtifactsConfiguration"/> that will be modified, validated and returned from Build</param>
        /// <param name="artifactTypes">A list of <see cref="ArtifactType"/> which represents the different artifact types</param>
        /// <param name="logger"></param>
        public ArtifactsConfigurationBuilder(Type[] artifacts, ArtifactsConfiguration artifactsConfiguration, DolittleArtifactTypes artifactTypes, IBuildToolLogger logger)
        {
            _artifacts = artifacts;
            _logger = logger;

            _artifactsConfiguration = artifactsConfiguration;
            _artifactTypes = artifactTypes;

        }

        /// <summary>
        /// Builds a valid <see cref="ArtifactsConfiguration"/> based on a <see cref="BoundedContextTopology"/> 
        /// </summary>
        /// <param name="boundedContextTopology"></param>
        /// <returns></returns>
        public ArtifactsConfiguration Build(BoundedContextTopology boundedContextTopology)
        {
            var newArtifacts = 0;

            var nonMatchingArtifacts = new List<string>();
            foreach (var artifactType in _artifactTypes.ArtifactTypes) 
            {
                newArtifacts += HandleArtifactOfType(
                    artifactType.Type,
                    boundedContextTopology,
                    artifactType.TypeName,
                    artifactType.TargetPropertyExpression,
                    ref nonMatchingArtifacts
                );
            }
            if (nonMatchingArtifacts.Any())
            {
                foreach (var artifactNamespace in nonMatchingArtifacts)
                    _logger.Warning($"An artifact with namespace: '{artifactNamespace}' could not be matched with any feature in the Bounded Context's topology");
                
                throw new NonMatchingArtifact();
            }
            
            _artifactsConfiguration.ValidateArtifacts(boundedContextTopology, _artifacts, _logger);

            if (newArtifacts > 0)
            {
                Program.NewArtifacts = true;
                _logger.Information($"Added {newArtifacts} new artifacts to the map.");
            }
            else 
                _logger.Information($"No new artifacts added to the map.");
            
            return _artifactsConfiguration;
        }

        int HandleArtifactOfType(Type artifactType, BoundedContextTopology boundedContextConfiguration, string artifactTypeName, Expression<Func<ArtifactsByTypeDefinition, IEnumerable<ArtifactDefinition>> > targetPropertyExpression, ref List<string> nonMatchingArtifacts)
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

                    if (_artifactsConfiguration.Artifacts.ContainsKey(feature.Feature))
                        artifactsByTypeDefinition = _artifactsConfiguration.Artifacts[feature.Feature];
                    else
                    {
                        artifactsByTypeDefinition = new ArtifactsByTypeDefinition();
                        _artifactsConfiguration.Artifacts[feature.Feature] = artifactsByTypeDefinition;
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