/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/

using System;
using System.Linq;
using Dolittle.Artifacts.Configuration;
using Machine.Specifications;

namespace Dolittle.Build.Artifact.for_Artifact.for_ArtifactConfigurationBuilder.when_using_modules
{
    public class when_building_artifacts_with_an_empty_ArtifactsConfiguration : given.an_empty_ArtifactsConfiguration
    {
         static readonly Type[] all_used_types = new []
        {
            typeof(Specs.Module.Feature.TheCommand), typeof(Specs.Module.Feature.TheEvent),
            typeof(Specs.Module.Feature.TheQuery), typeof(Specs.Module.Feature.TheReadModel),
            typeof(Specs.Module.Feature.TheEventSource), typeof(Specs.Module.Feature3.TheCommand), 
            typeof(Specs.Module.Feature3.TheEvent), typeof(Specs.Module.Feature3.TheQuery),
            typeof(Specs.Module.Feature3.TheReadModel), typeof(Specs.Module.Feature3.TheEventSource),
        };
        static readonly Type[] too_few_types = new []
        {
            typeof(Specs.Module.Feature.TheCommand), typeof(Specs.Module.Feature.TheEvent),
        };

        static ArtifactsConfigurationBuilder artifacts_configuration_builder_with_all_used_types = new ArtifactsConfigurationBuilder(all_used_types, artifacts_configuration, artifact_types, logger);
        static ArtifactsConfigurationBuilder artifacts_configuration_builder_with_too_few_types = new ArtifactsConfigurationBuilder(too_few_types, artifacts_configuration, artifact_types, logger);

        static ArtifactsConfiguration result_configuration;
        static Exception result_exception;
        
        Establish context = () => 
        {
            artifacts_configuration_builder_with_all_used_types = new ArtifactsConfigurationBuilder(all_used_types, artifacts_configuration, artifact_types, logger);
            artifacts_configuration_builder_with_too_few_types = new ArtifactsConfigurationBuilder(too_few_types, artifacts_configuration, artifact_types, logger);
        };
        Because of = () => 
        {
            result_configuration = artifacts_configuration_builder_with_all_used_types.Build(bounded_context_config);
            result_exception = Catch.Exception( () => artifacts_configuration_builder_with_too_few_types.Build(bounded_context_config));
        };

        It should_generate_an_artifacts_configuration_when_building_with_all_used_types = () => result_configuration.ShouldNotBeNull();
        It should_generate_an_artifacts_configuration_with_feature1 = () => result_configuration.Artifacts.ContainsKey(feature1).ShouldBeTrue();
        It should_generate_an_artifacts_configuration_with_one_command_artifact_under_feature1 = () => result_configuration.Artifacts[feature1].Commands.Count().ShouldEqual(1);
        It should_generate_an_artifacts_configuration_with_one_event_artifact_under_feature1 = () => result_configuration.Artifacts[feature1].Events.Count().ShouldEqual(1);
        It should_generate_an_artifacts_configuration_with_one_query_artifact_under_feature1 = () => result_configuration.Artifacts[feature1].Queries.Count().ShouldEqual(1);
        It should_generate_an_artifacts_configuration_with_one_read_model_artifact_under_feature1 = () => result_configuration.Artifacts[feature1].ReadModels.Count().ShouldEqual(1);
        It should_generate_an_artifacts_configuration_with_one_event_source_artifact_under_feature1 = () => result_configuration.Artifacts[feature1].EventSources.Count().ShouldEqual(1);
        It should_generate_an_artifacts_configuration_with_one_command_where_the_clr_type_is_correct_under_feature1 = () => result_configuration.Artifacts[feature1].Commands.First().Type.TypeString.ShouldEqual(ClrType.FromType(typeof(Specs.Module.Feature.TheCommand)).TypeString);
        It should_generate_an_artifacts_configuration_with_one_event_where_the_clr_type_is_correct_under_feature1 = () => result_configuration.Artifacts[feature1].Events.First().Type.TypeString.ShouldEqual(ClrType.FromType(typeof(Specs.Module.Feature.TheEvent)).TypeString); 
        It should_generate_an_artifacts_configuration_with_one_query_where_the_clr_type_is_correct_under_feature1 = () => result_configuration.Artifacts[feature1].Queries.First().Type.TypeString.ShouldEqual(ClrType.FromType(typeof(Specs.Module.Feature.TheQuery)).TypeString); 
        It should_generate_an_artifacts_configuration_with_one_read_model_where_the_clr_type_is_correct_under_feature1 = () => result_configuration.Artifacts[feature1].ReadModels.First().Type.TypeString.ShouldEqual(ClrType.FromType(typeof(Specs.Module.Feature.TheReadModel)).TypeString); 
        It should_generate_an_artifacts_configuration_with_one_event_source_where_the_clr_type_is_correct_under_feature1 = () => result_configuration.Artifacts[feature1].EventSources.First().Type.TypeString.ShouldEqual(ClrType.FromType(typeof(Specs.Module.Feature.TheEventSource)).TypeString); 
        

        It should_generate_an_artifacts_configuration_with_feature2 = () => result_configuration.Artifacts.ContainsKey(feature2).ShouldBeTrue();
        It should_generate_an_artifacts_configuration_with_one_command_artifact_under_feature2 = () => result_configuration.Artifacts[feature2].Commands.Count().ShouldEqual(1);
        It should_generate_an_artifacts_configuration_with_one_event_artifact_under_feature2 = () => result_configuration.Artifacts[feature2].Events.Count().ShouldEqual(1);
        It should_generate_an_artifacts_configuration_with_one_query_artifact_under_feature2 = () => result_configuration.Artifacts[feature2].Queries.Count().ShouldEqual(1);
        It should_generate_an_artifacts_configuration_with_one_read_model_artifact_under_feature2 = () => result_configuration.Artifacts[feature2].ReadModels.Count().ShouldEqual(1);
        It should_generate_an_artifacts_configuration_with_one_event_source_artifact_under_feature2 = () => result_configuration.Artifacts[feature2].EventSources.Count().ShouldEqual(1);
        It should_generate_an_artifacts_configuration_with_one_command_where_the_clr_type_is_correct_under_feature2 = () => result_configuration.Artifacts[feature2].Commands.First().Type.TypeString.ShouldEqual(ClrType.FromType(typeof(Specs.Module.Feature3.TheCommand)).TypeString);
        It should_generate_an_artifacts_configuration_with_one_event_where_the_clr_type_is_correct_under_feature2 = () => result_configuration.Artifacts[feature2].Events.First().Type.TypeString.ShouldEqual(ClrType.FromType(typeof(Specs.Module.Feature3.TheEvent)).TypeString); 
        It should_generate_an_artifacts_configuration_with_one_query_where_the_clr_type_is_correct_under_feature2 = () => result_configuration.Artifacts[feature2].Queries.First().Type.TypeString.ShouldEqual(ClrType.FromType(typeof(Specs.Module.Feature3.TheQuery)).TypeString); 
        It should_generate_an_artifacts_configuration_with_one_read_model_where_the_clr_type_is_correct_under_feature2 = () => result_configuration.Artifacts[feature2].ReadModels.First().Type.TypeString.ShouldEqual(ClrType.FromType(typeof(Specs.Module.Feature3.TheReadModel)).TypeString); 
        It should_generate_an_artifacts_configuration_with_one_event_source_where_the_clr_type_is_correct_under_feature2 = () => result_configuration.Artifacts[feature2].EventSources.First().Type.TypeString.ShouldEqual(ClrType.FromType(typeof(Specs.Module.Feature3.TheEventSource)).TypeString); 
        
        It should_throw_an_exception_when_building_an_artifacts_configuration_with_too_few_types = () => result_exception.ShouldNotBeNull();
        It should_throw_ArtifactNoLongerInStructure = () => result_exception.ShouldBeOfExactType(typeof(ArtifactNoLongerInStructure));
    }
}