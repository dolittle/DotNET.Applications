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

namespace Dolittle.Build.Artifact.for_Artifact.for_ArtifactsConfigurationExtensions.for_ValidateArtifacts.when_not_using_modules.given
{
    public class an_artifacts_configuration_with_duplicate_ids : given.a_bounded_context_config
    {
        protected readonly static ArtifactId duplicate_id = Guid.NewGuid();
        protected static ArtifactsConfiguration artifacts_configuration;

        static ArtifactsByTypeDefinition feature1_artifacts_definition_by_type;
        static ArtifactsByTypeDefinition feature2_artifacts_definition_by_type;

        static bool is_established = false;
        Establish context = () => 
        {
            if (is_established) return;
            is_established = true;

            var command_artifact_1 = new KeyValuePair<ArtifactId, ArtifactDefinition>(duplicate_id, new ArtifactDefinition(ArtifactGeneration.First, ClrType.FromType(typeof(Specs.Feature.TheCommand))));
            var command_artifact_2 = new KeyValuePair<ArtifactId, ArtifactDefinition>(Guid.NewGuid(), new ArtifactDefinition(ArtifactGeneration.First, ClrType.FromType(typeof(Specs.Feature3.TheCommand))));
            
            var event_artifact_1 = new KeyValuePair<ArtifactId, ArtifactDefinition>(Guid.NewGuid(), new ArtifactDefinition(ArtifactGeneration.First, ClrType.FromType(typeof(Specs.Feature.TheEvent))));
            var event_artifact_2 = new KeyValuePair<ArtifactId, ArtifactDefinition>(Guid.NewGuid(), new ArtifactDefinition(ArtifactGeneration.First, ClrType.FromType(typeof(Specs.Feature3.TheEvent))));
            
            var query_artifact_1 = new KeyValuePair<ArtifactId, ArtifactDefinition>(Guid.NewGuid(), new ArtifactDefinition(ArtifactGeneration.First, ClrType.FromType(typeof(Specs.Feature.TheQuery))));
            var query_artifact_2 = new KeyValuePair<ArtifactId, ArtifactDefinition>(Guid.NewGuid(), new ArtifactDefinition(ArtifactGeneration.First, ClrType.FromType(typeof(Specs.Feature3.TheQuery))));

            var read_model_artifact_1 = new KeyValuePair<ArtifactId, ArtifactDefinition>(duplicate_id, new ArtifactDefinition(ArtifactGeneration.First, ClrType.FromType(typeof(Specs.Feature.TheReadModel))));
            var read_model_artifact_2 = new KeyValuePair<ArtifactId, ArtifactDefinition>(Guid.NewGuid(), new ArtifactDefinition(ArtifactGeneration.First, ClrType.FromType(typeof(Specs.Feature3.TheReadModel))));

            var event_source_artifact_1 = new KeyValuePair<ArtifactId, ArtifactDefinition>(Guid.NewGuid(), new ArtifactDefinition(ArtifactGeneration.First, ClrType.FromType(typeof(Specs.Feature.TheEventSource))));
            var event_source_artifact_2 = new KeyValuePair<ArtifactId, ArtifactDefinition>(Guid.NewGuid(), new ArtifactDefinition(ArtifactGeneration.First, ClrType.FromType(typeof(Specs.Feature3.TheEventSource))));

            feature1_artifacts_definition_by_type = new ArtifactsByTypeDefinition(
                new Dictionary<ArtifactId, ArtifactDefinition>(new [] {command_artifact_1}),
                new Dictionary<ArtifactId, ArtifactDefinition>(new [] {event_artifact_1}),
                new Dictionary<ArtifactId, ArtifactDefinition>(new [] {event_source_artifact_1}),
                new Dictionary<ArtifactId, ArtifactDefinition>(new [] {read_model_artifact_1}),
                new Dictionary<ArtifactId, ArtifactDefinition>(new [] {query_artifact_1})
            );

            feature2_artifacts_definition_by_type = new ArtifactsByTypeDefinition(
                new Dictionary<ArtifactId, ArtifactDefinition>(new [] {command_artifact_2}),
                new Dictionary<ArtifactId, ArtifactDefinition>(new [] {event_artifact_2}),
                new Dictionary<ArtifactId, ArtifactDefinition>(new [] {event_source_artifact_2}),
                new Dictionary<ArtifactId, ArtifactDefinition>(new [] {read_model_artifact_2}),
                new Dictionary<ArtifactId, ArtifactDefinition>(new [] {query_artifact_2})
            );

            artifacts_configuration = new ArtifactsConfiguration(new Dictionary<Feature, ArtifactsByTypeDefinition>{
                {feature1, feature1_artifacts_definition_by_type},
                {feature2, feature2_artifacts_definition_by_type}
            });
        };   
    }
}