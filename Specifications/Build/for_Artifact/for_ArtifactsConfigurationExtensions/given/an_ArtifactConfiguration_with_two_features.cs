/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/

using System;
using System.Collections.Generic;
using Dolittle.Applications;
using Dolittle.Artifacts;
using Dolittle.Artifacts.Configuration;
using Machine.Specifications;

namespace Dolittle.Build.Artifact.for_Artifact.for_ArtifactsConfigurationExtensions.given
{
    public class an_ArtifactConfiguration_with_two_features : Dolittle.Build.given.an_ILogger
    {
        protected static readonly DolittleArtifactTypes artifact_types = new DolittleArtifactTypes();
        protected static readonly Feature feature1 = Guid.NewGuid();
        protected static readonly Feature feature2 = Guid.NewGuid();
        
        protected static readonly List<ArtifactDefinition> all_artifact_definitions = new List<ArtifactDefinition>();

        protected static readonly List<ArtifactDefinition> all_artifact_definitions_of_feature1 = new List<ArtifactDefinition>();
        protected static readonly List<ArtifactDefinition> all_artifact_definitions_of_feature2 = new List<ArtifactDefinition>();

        protected static readonly ArtifactsByTypeDefinition feature1_artifacts_definition_by_type = new ArtifactsByTypeDefinition();
        protected static readonly ArtifactsByTypeDefinition feature2_artifacts_definition_by_type = new ArtifactsByTypeDefinition();
        
        protected static readonly ArtifactsConfiguration artifact_configuration = new ArtifactsConfiguration();

        protected static readonly List<ArtifactDefinition> all_commands = new List<ArtifactDefinition>();
        protected static readonly List<ArtifactDefinition> all_events = new List<ArtifactDefinition>();
        protected static readonly List<ArtifactDefinition> all_queries = new List<ArtifactDefinition>();
        protected static readonly List<ArtifactDefinition> all_read_models = new List<ArtifactDefinition>();
        protected static readonly List<ArtifactDefinition> all_event_sources = new List<ArtifactDefinition>();
        static bool is_established = false;
        Establish context = () => 
        {
            if (is_established) return;
            is_established = true;
        
            var command_artifact_1 = new ArtifactDefinition(){Artifact = Guid.NewGuid(), Generation = ArtifactGeneration.First, Type = ClrType.FromType(typeof(Specs.Feature.TheCommand))};
            var command_artifact_2 = new ArtifactDefinition(){Artifact = Guid.NewGuid(), Generation = ArtifactGeneration.First, Type = ClrType.FromType(typeof(Specs.Feature3.TheCommand))};
            all_commands.AddRange(new [] {command_artifact_1, command_artifact_2});
            
            var event_artifact_1 = new ArtifactDefinition(){Artifact = Guid.NewGuid(), Generation = ArtifactGeneration.First, Type = ClrType.FromType(typeof(Specs.Feature.TheEvent))};
            var event_artifact_2 = new ArtifactDefinition(){Artifact = Guid.NewGuid(), Generation = ArtifactGeneration.First, Type = ClrType.FromType(typeof(Specs.Feature3.TheEvent))};
            all_events.AddRange(new [] {event_artifact_1, event_artifact_2});
            
            var query_artifact_1 = new ArtifactDefinition(){Artifact = Guid.NewGuid(), Generation = ArtifactGeneration.First, Type = ClrType.FromType(typeof(Specs.Feature.TheQuery))};
            var query_artifact_2 = new ArtifactDefinition(){Artifact = Guid.NewGuid(), Generation = ArtifactGeneration.First, Type = ClrType.FromType(typeof(Specs.Feature3.TheQuery))};
            all_queries.AddRange(new [] {query_artifact_1, query_artifact_2});

            var read_model_artifact_1 = new ArtifactDefinition(){Artifact = Guid.NewGuid(), Generation = ArtifactGeneration.First, Type = ClrType.FromType(typeof(Specs.Feature.TheReadModel))};
            var read_model_artifact_2 = new ArtifactDefinition(){Artifact = Guid.NewGuid(), Generation = ArtifactGeneration.First, Type = ClrType.FromType(typeof(Specs.Feature3.TheReadModel))};
            all_read_models.AddRange(new [] {read_model_artifact_1, read_model_artifact_2});

            var event_source_artifact_1 = new ArtifactDefinition(){Artifact = Guid.NewGuid(), Generation = ArtifactGeneration.First, Type = ClrType.FromType(typeof(Specs.Feature.TheEventSource))};
            var event_source_artifact_2 = new ArtifactDefinition(){Artifact = Guid.NewGuid(), Generation = ArtifactGeneration.First, Type = ClrType.FromType(typeof(Specs.Feature3.TheEventSource))};
            all_event_sources.AddRange(new [] {event_source_artifact_1, event_source_artifact_2});

            all_artifact_definitions_of_feature1.AddRange(new []
            {
                command_artifact_1,
                event_artifact_1,
                query_artifact_1,
                read_model_artifact_1,
                event_source_artifact_1,
            });
            all_artifact_definitions_of_feature2.AddRange(new []
            {
                command_artifact_2,
                event_artifact_2,
                query_artifact_2,
                read_model_artifact_2,
                event_source_artifact_2,
            });
            all_artifact_definitions.AddRange(all_artifact_definitions_of_feature1);

            all_artifact_definitions.AddRange(all_artifact_definitions_of_feature2);

            feature1_artifacts_definition_by_type.Commands = new []{command_artifact_1};
            feature1_artifacts_definition_by_type.Events = new []{event_artifact_1};
            feature1_artifacts_definition_by_type.Queries = new []{query_artifact_1};
            feature1_artifacts_definition_by_type.ReadModels = new []{read_model_artifact_1};
            feature1_artifacts_definition_by_type.EventSources = new []{event_source_artifact_1};


            feature2_artifacts_definition_by_type.Commands = new []{command_artifact_2};
            feature2_artifacts_definition_by_type.Events = new []{event_artifact_2};
            feature2_artifacts_definition_by_type.Queries = new []{query_artifact_2};
            feature2_artifacts_definition_by_type.ReadModels = new []{read_model_artifact_2};
            feature2_artifacts_definition_by_type.EventSources = new []{event_source_artifact_2};

            artifact_configuration.Artifacts.Add(feature1, feature1_artifacts_definition_by_type);

            artifact_configuration.Artifacts.Add(feature2, feature2_artifacts_definition_by_type);

        };
    }
}