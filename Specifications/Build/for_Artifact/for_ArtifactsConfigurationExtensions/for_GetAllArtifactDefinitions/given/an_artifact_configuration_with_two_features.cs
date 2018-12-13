/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/

using System;
using System.Collections.Generic;
using System.Linq;
using Dolittle.Applications;
using Dolittle.Artifacts;
using Dolittle.Artifacts.Configuration;
using Machine.Specifications;

namespace Dolittle.Build.Artifact.for_Artifact.for_ArtifactsConfigurationExtensions.for_GetAllArtifactDefinitions.given
{
    public class an_artifact_configuration_with_two_features : Dolittle.Build.given.an_ILogger
    {
        protected static readonly DolittleArtifactTypes artifact_types = new DolittleArtifactTypes();
        protected static readonly Feature feature1 = Guid.NewGuid();
        protected static readonly Feature feature2 = Guid.NewGuid();
        

        protected static readonly ArtifactsByTypeDefinition first_feature_artifacts_definition_by_type = new ArtifactsByTypeDefinition();
        protected static readonly ArtifactsByTypeDefinition second_feature_artifacts_definition_by_type = new ArtifactsByTypeDefinition();
        
        protected static ArtifactsConfiguration artifacts_configuration;

        protected static IEnumerable<ArtifactDefinition> all_artifact_definitions;
        protected static IEnumerable<ArtifactDefinition> all_artifact_definitions_of_first_feature;
        protected static IEnumerable<ArtifactDefinition> all_artifact_definitions_of_second_feature;
        protected static IEnumerable<ArtifactDefinition> all_commands;
        protected static IEnumerable<ArtifactDefinition> all_events;
        protected static IEnumerable<ArtifactDefinition> all_queries;
        protected static IEnumerable<ArtifactDefinition> all_read_models;
        protected static IEnumerable<ArtifactDefinition> all_event_sources;
        static bool is_established = false;
        Establish context = () => 
        {
            if (is_established) return;
            is_established = true;


            var command_artifact_1 = new ArtifactDefinition(){Artifact = Guid.NewGuid(), Generation = ArtifactGeneration.First, Type = ClrType.FromType(typeof(Specs.Feature.TheCommand))};
            var command_artifact_2 = new ArtifactDefinition(){Artifact = Guid.NewGuid(), Generation = ArtifactGeneration.First, Type = ClrType.FromType(typeof(Specs.Feature3.TheCommand))};
            all_commands = new [] {command_artifact_1, command_artifact_2};
            
            var event_artifact_1 = new ArtifactDefinition(){Artifact = Guid.NewGuid(), Generation = ArtifactGeneration.First, Type = ClrType.FromType(typeof(Specs.Feature.TheEvent))};
            var event_artifact_2 = new ArtifactDefinition(){Artifact = Guid.NewGuid(), Generation = ArtifactGeneration.First, Type = ClrType.FromType(typeof(Specs.Feature3.TheEvent))};
            all_events = new [] {event_artifact_1, event_artifact_2};
            
            var query_artifact_1 = new ArtifactDefinition(){Artifact = Guid.NewGuid(), Generation = ArtifactGeneration.First, Type = ClrType.FromType(typeof(Specs.Feature.TheQuery))};
            var query_artifact_2 = new ArtifactDefinition(){Artifact = Guid.NewGuid(), Generation = ArtifactGeneration.First, Type = ClrType.FromType(typeof(Specs.Feature3.TheQuery))};
            all_queries = new [] {query_artifact_1, query_artifact_2};

            var read_model_artifact_1 = new ArtifactDefinition(){Artifact = Guid.NewGuid(), Generation = ArtifactGeneration.First, Type = ClrType.FromType(typeof(Specs.Feature.TheReadModel))};
            var read_model_artifact_2 = new ArtifactDefinition(){Artifact = Guid.NewGuid(), Generation = ArtifactGeneration.First, Type = ClrType.FromType(typeof(Specs.Feature3.TheReadModel))};
            all_read_models = new [] {read_model_artifact_1, read_model_artifact_2};

            var event_source_artifact_1 = new ArtifactDefinition(){Artifact = Guid.NewGuid(), Generation = ArtifactGeneration.First, Type = ClrType.FromType(typeof(Specs.Feature.TheEventSource))};
            var event_source_artifact_2 = new ArtifactDefinition(){Artifact = Guid.NewGuid(), Generation = ArtifactGeneration.First, Type = ClrType.FromType(typeof(Specs.Feature3.TheEventSource))};
            all_event_sources = new [] {event_source_artifact_1, event_source_artifact_2};

            all_artifact_definitions_of_first_feature = new[]
            {
                command_artifact_1,
                event_artifact_1,
                query_artifact_1,
                read_model_artifact_1,
                event_source_artifact_1,
            };
            all_artifact_definitions_of_second_feature = new []
            {
                command_artifact_2,
                event_artifact_2,
                query_artifact_2,
                read_model_artifact_2,
                event_source_artifact_2,
            };
            all_artifact_definitions = all_artifact_definitions_of_first_feature.Concat(all_artifact_definitions_of_second_feature);

            first_feature_artifacts_definition_by_type.Commands = new []{command_artifact_1};
            first_feature_artifacts_definition_by_type.Events = new []{event_artifact_1};
            first_feature_artifacts_definition_by_type.Queries = new []{query_artifact_1};
            first_feature_artifacts_definition_by_type.ReadModels = new []{read_model_artifact_1};
            first_feature_artifacts_definition_by_type.EventSources = new []{event_source_artifact_1};


            second_feature_artifacts_definition_by_type.Commands = new []{command_artifact_2};
            second_feature_artifacts_definition_by_type.Events = new []{event_artifact_2};
            second_feature_artifacts_definition_by_type.Queries = new []{query_artifact_2};
            second_feature_artifacts_definition_by_type.ReadModels = new []{read_model_artifact_2};
            second_feature_artifacts_definition_by_type.EventSources = new []{event_source_artifact_2};

            artifacts_configuration = new ArtifactsConfiguration(new Dictionary<Feature, ArtifactsByTypeDefinition>{
                {feature1, first_feature_artifacts_definition_by_type},
                {feature2, second_feature_artifacts_definition_by_type}
            });
        };
    }
}