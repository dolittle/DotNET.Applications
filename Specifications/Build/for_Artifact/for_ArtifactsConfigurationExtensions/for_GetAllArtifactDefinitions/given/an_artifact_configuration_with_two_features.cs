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
        

        protected static ArtifactsByTypeDefinition first_feature_artifacts_definition_by_type;
        protected static ArtifactsByTypeDefinition second_feature_artifacts_definition_by_type;
        
        protected static ArtifactsConfiguration artifacts_configuration;

        protected static IDictionary<ArtifactId, ArtifactDefinition> all_artifact_definitions;
        protected static IDictionary<ArtifactId, ArtifactDefinition> all_artifact_definitions_of_first_feature;
        protected static IDictionary<ArtifactId, ArtifactDefinition> all_artifact_definitions_of_second_feature;
        protected static IDictionary<ArtifactId, ArtifactDefinition> all_commands;
        protected static IDictionary<ArtifactId, ArtifactDefinition> all_events;
        protected static IDictionary<ArtifactId, ArtifactDefinition> all_queries;
        protected static IDictionary<ArtifactId, ArtifactDefinition> all_read_models;
        protected static IDictionary<ArtifactId, ArtifactDefinition> all_event_sources;
        static bool is_established = false;
        Establish context = () => 
        {
            if (is_established) return;
            is_established = true;

            var command_artifact_1 = new KeyValuePair<ArtifactId, ArtifactDefinition>(Guid.NewGuid(), new ArtifactDefinition(ArtifactGeneration.First, ClrType.FromType(typeof(Specs.Feature.TheCommand))));
            var command_artifact_2 = new KeyValuePair<ArtifactId, ArtifactDefinition>(Guid.NewGuid(), new ArtifactDefinition(ArtifactGeneration.First, ClrType.FromType(typeof(Specs.Feature3.TheCommand))));
            all_commands = new Dictionary<ArtifactId, ArtifactDefinition>(new [] {command_artifact_1, command_artifact_2});
            
            var event_artifact_1 = new KeyValuePair<ArtifactId, ArtifactDefinition>(Guid.NewGuid(), new ArtifactDefinition(ArtifactGeneration.First, ClrType.FromType(typeof(Specs.Feature.TheEvent))));
            var event_artifact_2 = new KeyValuePair<ArtifactId, ArtifactDefinition>(Guid.NewGuid(), new ArtifactDefinition(ArtifactGeneration.First, ClrType.FromType(typeof(Specs.Feature3.TheEvent))));
            all_events = new Dictionary<ArtifactId, ArtifactDefinition>(new [] {event_artifact_1, event_artifact_2});
            
            var query_artifact_1 = new KeyValuePair<ArtifactId, ArtifactDefinition>(Guid.NewGuid(), new ArtifactDefinition(ArtifactGeneration.First, ClrType.FromType(typeof(Specs.Feature.TheQuery))));
            var query_artifact_2 = new KeyValuePair<ArtifactId, ArtifactDefinition>(Guid.NewGuid(), new ArtifactDefinition(ArtifactGeneration.First, ClrType.FromType(typeof(Specs.Feature3.TheQuery))));
            all_queries = new Dictionary<ArtifactId, ArtifactDefinition>(new [] {query_artifact_1, query_artifact_2});

            var read_model_artifact_1 = new KeyValuePair<ArtifactId, ArtifactDefinition>(Guid.NewGuid(), new ArtifactDefinition(ArtifactGeneration.First, ClrType.FromType(typeof(Specs.Feature.TheReadModel))));
            var read_model_artifact_2 = new KeyValuePair<ArtifactId, ArtifactDefinition>(Guid.NewGuid(), new ArtifactDefinition(ArtifactGeneration.First, ClrType.FromType(typeof(Specs.Feature3.TheReadModel))));
            all_read_models = new Dictionary<ArtifactId, ArtifactDefinition>(new [] {read_model_artifact_1, read_model_artifact_2});

            var event_source_artifact_1 = new KeyValuePair<ArtifactId, ArtifactDefinition>(Guid.NewGuid(), new ArtifactDefinition(ArtifactGeneration.First, ClrType.FromType(typeof(Specs.Feature.TheEventSource))));
            var event_source_artifact_2 = new KeyValuePair<ArtifactId, ArtifactDefinition>(Guid.NewGuid(), new ArtifactDefinition(ArtifactGeneration.First, ClrType.FromType(typeof(Specs.Feature3.TheEventSource))));
            all_event_sources = new Dictionary<ArtifactId, ArtifactDefinition>(new [] {event_source_artifact_1, event_source_artifact_2});

            all_artifact_definitions_of_first_feature = new Dictionary<ArtifactId, ArtifactDefinition>(new [] {
                command_artifact_1,
                event_artifact_1,
                query_artifact_1,
                read_model_artifact_1,
                event_source_artifact_1,
            });

            all_artifact_definitions_of_second_feature = new Dictionary<ArtifactId, ArtifactDefinition>(new [] {
                command_artifact_2,
                event_artifact_2,
                query_artifact_2,
                read_model_artifact_2,
                event_source_artifact_2,
            });

            all_artifact_definitions = new Dictionary<ArtifactId, ArtifactDefinition>(
                all_artifact_definitions_of_first_feature.Concat(all_artifact_definitions_of_second_feature)
            );

            first_feature_artifacts_definition_by_type = new ArtifactsByTypeDefinition(
                new Dictionary<ArtifactId, ArtifactDefinition>(new [] {command_artifact_1}),
                new Dictionary<ArtifactId, ArtifactDefinition>(new [] {event_artifact_1}),
                new Dictionary<ArtifactId, ArtifactDefinition>(new [] {event_source_artifact_1}),
                new Dictionary<ArtifactId, ArtifactDefinition>(new [] {read_model_artifact_1}),
                new Dictionary<ArtifactId, ArtifactDefinition>(new [] {query_artifact_1})
            );

            second_feature_artifacts_definition_by_type = new ArtifactsByTypeDefinition(
                new Dictionary<ArtifactId, ArtifactDefinition>(new [] {command_artifact_2}),
                new Dictionary<ArtifactId, ArtifactDefinition>(new [] {event_artifact_2}),
                new Dictionary<ArtifactId, ArtifactDefinition>(new [] {event_source_artifact_2}),
                new Dictionary<ArtifactId, ArtifactDefinition>(new [] {read_model_artifact_2}),
                new Dictionary<ArtifactId, ArtifactDefinition>(new [] {query_artifact_2})
            );

            artifacts_configuration = new ArtifactsConfiguration(new Dictionary<Feature, ArtifactsByTypeDefinition>{
                {feature1, first_feature_artifacts_definition_by_type},
                {feature2, second_feature_artifacts_definition_by_type}
            });
        };
    }
}