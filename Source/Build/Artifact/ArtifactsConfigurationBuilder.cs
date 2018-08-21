/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Dolittle.Applications.Configuration;
using Dolittle.Artifacts;
using Dolittle.Artifacts.Configuration;
using Dolittle.Collections;
using Dolittle.Logging;
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
        readonly ILogger _logger;
        readonly IEnumerable<ArtifactType> _artifactTypes;
        ArtifactsConfiguration _artifactsConfiguration;

        /// <summary>
        /// Instantiates an instance of <see cref="ArtifactsConfigurationBuilder"/>
        /// </summary>
        /// <param name="artifacts">The discovered types of artifacts in the Bounded Context's assemblies</param>
        /// <param name="artifactsConfiguration">The <see cref="ArtifactsConfiguration"/> that will be modified, validated and returned from Build</param>
        /// <param name="artifactTypes">A list of <see cref="ArtifactType"/> which represents the different artifact types</param>
        /// <param name="logger"></param>
        public ArtifactsConfigurationBuilder(Type[] artifacts, ArtifactsConfiguration artifactsConfiguration, IEnumerable<ArtifactType> artifactTypes, ILogger logger)
        {
            _artifacts = artifacts;
            _logger = logger;

            _artifactsConfiguration = artifactsConfiguration;
            _artifactTypes = artifactTypes;

        }

        /// <summary>
        /// Builds a valid <see cref="ArtifactsConfiguration"/> based on a <see cref="BoundedContextConfiguration"/> 
        /// </summary>
        /// <param name="boundedContextConfiguration"></param>
        /// <returns></returns>
        public ArtifactsConfiguration Build(BoundedContextConfiguration boundedContextConfiguration)
        {
            _logger.Information("Building artifacts");
            var startTime = DateTime.UtcNow;
            
            var newArtifacts = 0;

            var nonMatchingArtifacts = new List<string>();
            foreach (var artifactType in _artifactTypes) 
            {
                newArtifacts += HandleArtifactOfType(
                    artifactType.Type,
                    boundedContextConfiguration,
                    artifactType.TypeName,
                    artifactType.TargetPropertyExpression,
                    ref nonMatchingArtifacts
                );
            }
            if (nonMatchingArtifacts.Any())
            {
                foreach (var artifactNamespace in nonMatchingArtifacts)
                    _logger.Warning($"An artifact with namespace = {artifactNamespace} could not be matched with any feature in the Bounded Context Configuration's topology");
                
                throw new NonMatchingArtifact();
            }
            
            _artifactsConfiguration.ValidateArtifacts(boundedContextConfiguration, _artifacts, _logger);

            var endTime = DateTime.UtcNow;
            var deltaTime = endTime.Subtract(startTime);

            if (newArtifacts > 0)
            {
                Program.NewArtifacts = true;
                _logger.Information($"Added {newArtifacts} artifacts to the map.");
            }
            else 
                _logger.Information($"No new artifacts added to the map.");

            _logger.Information($"Finished artifacts build process. (Took {deltaTime.TotalSeconds} seconds)");
            
            return _artifactsConfiguration;
        }

        int HandleArtifactOfType(Type artifactType, BoundedContextConfiguration boundedContextConfiguration, string typeName, Expression<Func<ArtifactsByTypeDefinition, IEnumerable<ArtifactDefinition>> > targetPropertyExpression, ref List<string> nonMatchingArtifacts)
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
                        var newAndExistingArtifacts = new List<ArtifactDefinition>(existingArtifacts);
                        var artifactDefinition = new ArtifactDefinition
                        {
                            Artifact = ArtifactId.New(),
                            Generation = ArtifactGeneration.First,
                            Type = ClrType.FromType(artifact)
                        };
                        _logger.Debug($"Adding '{artifact.Name}' as a new {typeName} artifact with identifier '{artifactDefinition.Artifact}'");
                        newAndExistingArtifacts.Add(artifactDefinition);

                        newArtifacts++;

                        targetProperty.SetValue(artifactsByTypeDefinition, newAndExistingArtifacts);
                    }
                }
            }
            return newArtifacts;
        }
    }
}