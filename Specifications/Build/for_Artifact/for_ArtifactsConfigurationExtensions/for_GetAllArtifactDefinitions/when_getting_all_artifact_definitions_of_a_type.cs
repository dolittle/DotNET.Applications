/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using System.Collections.Generic;
using System.Linq;
using Dolittle.Artifacts;
using Dolittle.Artifacts.Configuration;
using Dolittle.Reflection;
using Machine.Specifications;

namespace Dolittle.Build.Artifact.for_Artifact.for_ArtifactsConfigurationExtensions.for_GetAllArtifactDefinitions
{
    public class when_getting_all_artifact_definitions_of_a_type: given.an_artifact_configuration_with_two_features
    {
        static IEnumerable<KeyValuePair<ArtifactId, ArtifactDefinition>> result_command_artifact_definitions;
        static IEnumerable<KeyValuePair<ArtifactId, ArtifactDefinition>> result_event_artifact_definitions;
        static IEnumerable<KeyValuePair<ArtifactId, ArtifactDefinition>> result_query_artifact_definitions;
        static IEnumerable<KeyValuePair<ArtifactId, ArtifactDefinition>> result_read_model_artifact_definitions;
        static IEnumerable<KeyValuePair<ArtifactId, ArtifactDefinition>> result_event_source_artifact_definitions;
        

        Because of = () => 
        {
            result_command_artifact_definitions = artifacts_configuration.GetAllArtifactDefinitions(artifact_types.ArtifactTypes.Single(_ => _.TypeName == "command").TargetPropertyExpression.GetPropertyInfo());
            result_event_artifact_definitions = artifacts_configuration.GetAllArtifactDefinitions(artifact_types.ArtifactTypes.Single(_ => _.TypeName == "event").TargetPropertyExpression.GetPropertyInfo());
            result_query_artifact_definitions = artifacts_configuration.GetAllArtifactDefinitions(artifact_types.ArtifactTypes.Single(_ => _.TypeName == "query").TargetPropertyExpression.GetPropertyInfo());
            result_read_model_artifact_definitions = artifacts_configuration.GetAllArtifactDefinitions(artifact_types.ArtifactTypes.Single(_ => _.TypeName == "read model").TargetPropertyExpression.GetPropertyInfo());
            result_event_source_artifact_definitions = artifacts_configuration.GetAllArtifactDefinitions(artifact_types.ArtifactTypes.Single(_ => _.TypeName == "event source").TargetPropertyExpression.GetPropertyInfo());
        };
        It should_have_the_same_amount_of_command_artifacts = () => result_command_artifact_definitions.Count().ShouldEqual(all_commands.Count());
        It should_have_the_same_command_artifacts = () => result_command_artifact_definitions.ShouldContain(all_commands);
        
        It should_have_the_same_amount_of_event_artifacts = () => result_event_artifact_definitions.Count().ShouldEqual(all_events.Count());
        It should_have_the_same_event_artifacts = () => result_event_artifact_definitions.ShouldContain(all_events);
        
        It should_have_the_same_amount_of_query_artifacts = () => result_query_artifact_definitions.Count().ShouldEqual(all_queries.Count());
        It should_have_the_same_query_artifacts = () => result_query_artifact_definitions.ShouldContain(all_queries);
        
        It should_have_the_same_amount_of_read_model_artifacts = () => result_read_model_artifact_definitions.Count().ShouldEqual(all_read_models.Count());
        It should_have_the_same_read_model_artifacts = () => result_read_model_artifact_definitions.ShouldContain(all_read_models);
        
        It should_have_the_same_amount_of_event_source_artifacts = () => result_event_source_artifact_definitions.Count().ShouldEqual(all_event_sources.Count());
        It should_have_the_same_event_source_artifacts = () => result_event_source_artifact_definitions.ShouldContain(all_event_sources);  
        
    }
}