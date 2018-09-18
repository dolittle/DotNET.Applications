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
        readonly DolittleArtifactTypes _artifactTypes;
        ArtifactsConfiguration _artifactsConfiguration;

        /// <summary>
        /// Instantiates an instance of <see cref="ArtifactsConfigurationBuilder"/>
        /// </summary>
        /// <param name="artifacts">The discovered types of artifacts in the Bounded Context's assemblies</param>
        /// <param name="artifactsConfiguration">The <see cref="ArtifactsConfiguration"/> that will be modified, validated and returned from Build</param>
        /// <param name="artifactTypes">A list of <see cref="ArtifactType"/> which represents the different artifact types</param>
        /// <param name="logger"></param>
        public ArtifactsConfigurationBuilder(Type[] artifacts, ArtifactsConfiguration artifactsConfiguration, DolittleArtifactTypes artifactTypes, ILogger logger)
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
            foreach (var artifactType in _artifactTypes.ArtifactTypes.Where(_ => !_.Type.Equals(typeof(Dolittle.Events.Processing.ICanProcessEvents)))) 
            {
                newArtifacts += HandleArtifactOfType(
                    artifactType.Type,
                    boundedContextConfiguration,
                    artifactType.TypeName,
                    artifactType.TargetPropertyExpression,
                    ref nonMatchingArtifacts
                );
            }
            newArtifacts += HandleEventProcessors(boundedContextConfiguration, ref nonMatchingArtifacts);
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

        int HandleArtifactOfType(Type artifactType, BoundedContextConfiguration boundedContextConfiguration, string artifactTypeName, Expression<Func<ArtifactsByTypeDefinition, IEnumerable<ArtifactDefinition>> > targetPropertyExpression, ref List<string> nonMatchingArtifacts)
        {
            if (artifactType.Equals(typeof(Dolittle.Events.Processing.ICanProcessEvents))) throw new ArgumentException("Eventprocessor artifacts should be handled differently ", "artifactType");
            
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

                            if (
                                ! ( existingArtifact.Artifact.Value.Equals(artifactObject.Id.Value)
                                && existingArtifact.Generation.Value.Equals(artifactObject.Generation.Value) )
                                )
                            {
                                SetNewAndExistingArtifacts(artifactObject, artifact, artifactsByTypeDefinition, targetProperty, existingArtifacts, artifactTypeName);
                                newArtifacts++;
                            }
                        }
                    }
                }
            }
            return newArtifacts;
        }
        int HandleEventProcessors(BoundedContextConfiguration boundedContextConfiguration, ref List<string> nonMatchingArtifacts)
        {
            var targetProperty = _artifactTypes.ArtifactTypes.SingleOrDefault(_ => _.Type.Equals(typeof(Dolittle.Events.Processing.ICanProcessEvents)))
                                                                .TargetPropertyExpression.GetPropertyInfo();
            var artifactType = typeof(Dolittle.Events.Processing.ICanProcessEvents);
            var artifactTypeName = "event processor";

            var newArtifacts = 0;
            var artifacts = _artifacts.Where(_ => artifactType.IsAssignableFrom(_));

            Dictionary<Guid, ClrType> usedEventProcessorIds = new Dictionary<Guid, ClrType>(); 

            var oldExistingEventProcessors = _artifactsConfiguration.GetAllArtifactDefinitions(targetProperty);

            foreach (var artifact in artifacts)
            {
                var feature = boundedContextConfiguration.FindMatchingFeature(artifact.Namespace, ref nonMatchingArtifacts);
                if (feature != null)
                {
                    ArtifactsByTypeDefinition artifactsByTypeDefinition;
                    List<ArtifactDefinition> eventProcessorMethods = new List<ArtifactDefinition>();

                    if (_artifactsConfiguration.Artifacts.ContainsKey(feature.Feature))
                        artifactsByTypeDefinition = _artifactsConfiguration.Artifacts[feature.Feature];
                    else
                    {
                        artifactsByTypeDefinition = new ArtifactsByTypeDefinition();
                        _artifactsConfiguration.Artifacts[feature.Feature] = artifactsByTypeDefinition;
                    }

                    var existingArtifactsInFeature = targetProperty.GetValue(artifactsByTypeDefinition) as IEnumerable<ArtifactDefinition>;

                    artifact.GetMethods().ForEach(method => 
                    {
                        var attribute = method.GetCustomAttribute<Dolittle.Events.Processing.EventProcessorAttribute>(false);

                        if (attribute != null) 
                        {
                            var artifactObject = new Dolittle.Artifacts.Artifact(attribute.Id, ArtifactGeneration.First);
                            var clrType = ClrType.FromType(artifact);
                            
                            if (usedEventProcessorIds.ContainsKey(attribute.Id))
                            {
                                var otherEventprocessor = usedEventProcessorIds[attribute.Id];
                                throw new DuplicateArtifact($"The Event Processor Id '{attribute.Id.ToString()}' used for Event Processor with ClrType: {clrType.TypeString} is already used by another Event Processor with ClrType: '{otherEventprocessor.TypeString}'");
                            }

                            if (! existingArtifactsInFeature.Any(_ => 
                                                                    _.Type.GetActualType().Equals(artifact) 
                                                                    && _.Artifact.Value.Equals(artifactObject.Id.Value)
                                                                    && _.Generation.Value.Equals(artifactObject.Generation.Value))
                                )
                            {
                                eventProcessorMethods.Add(new ArtifactDefinition()
                                {
                                    Artifact = artifactObject.Id,
                                    Generation = artifactObject.Generation,
                                    Type = clrType
                                });
                                usedEventProcessorIds.Add(artifactObject.Id.Value, clrType);
                                newArtifacts++;
                            }
                        }
                    });

                    if (! eventProcessorMethods.Any())
                    {
                        _logger.Warning($"There are not Event Processor methods in {artifact.FullName}. Only methods with the {typeof(Dolittle.Events.Processing.EventProcessorAttribute).FullName} can process Events");
                        continue;
                    }
                    
                    foreach (var artifactDefinition in eventProcessorMethods)
                    {
                        if (oldExistingEventProcessors.Any(_ => _.Artifact.Value.Equals(artifactDefinition.Artifact.Value)))
                        {
                            var oldEventProcessorArtifact = oldExistingEventProcessors.First(_ => _.Artifact.Value.Equals(artifactDefinition.Artifact.Value));
                            _logger.Warning($"There is an old Artifact definition for an Event Processor with ClrType {oldEventProcessorArtifact.Type.TypeString} that has the same Artifact Id as Event Processor with ClrType {artifactDefinition.Type.TypeString}. The Id is {artifactDefinition.Artifact.Value.ToString()}");
                        }
                    }

                    foreach (var artifactDefinition in eventProcessorMethods)
                        _logger.Trace($"Adding '{artifact.Name}' as a new {artifactTypeName} artifact with identifier '{artifactDefinition.Artifact}'");
                    
                    eventProcessorMethods.AddRange(existingArtifactsInFeature);
                    SetNewAndExistingArtifacts(eventProcessorMethods, artifact, artifactsByTypeDefinition, targetProperty, artifactTypeName);
                }
            }


            return newArtifacts;
        }
        void SetNewAndExistingArtifacts(IEnumerable<ArtifactDefinition> newAndExistingArtifacts, Type artifact, ArtifactsByTypeDefinition artifactsByTypeDefinition, PropertyInfo targetProperty, string artifactTypeName)
        {
            targetProperty.SetValue(artifactsByTypeDefinition, newAndExistingArtifacts);
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