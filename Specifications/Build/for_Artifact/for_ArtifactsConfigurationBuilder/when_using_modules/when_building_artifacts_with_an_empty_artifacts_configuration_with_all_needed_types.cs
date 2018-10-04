
/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/

using System;
using System.Collections.Generic;
using System.Linq;
using Dolittle.Artifacts.Configuration;
using Machine.Specifications;

namespace Dolittle.Build.Artifact.for_Artifact.for_ArtifactConfigurationBuilder.when_using_modules_modules
{
    public class when_building_artifacts_with_an_empty_artifacts_configuration_with_all_needed_types : given.an_empty_artifacts_configuration
    {
        static readonly IEnumerable<Type> all_used_types = new []
        {
            typeof(Specs.Module.Feature.TheCommand), typeof(Specs.Module.Feature.TheEvent),
            typeof(Specs.Module.Feature.TheQuery), typeof(Specs.Module.Feature.TheReadModel),
            typeof(Specs.Module.Feature.TheEventSource), typeof(Specs.Module.Feature3.TheCommand), 
            typeof(Specs.Module.Feature3.TheEvent), typeof(Specs.Module.Feature3.TheQuery),
            typeof(Specs.Module.Feature3.TheReadModel), typeof(Specs.Module.Feature3.TheEventSource),
        };
        static ArtifactsConfigurationBuilder artifacts_configuration_builder_with_all_used_types;
        
        static ArtifactsConfiguration result_configuration;
        
        Establish context = () => artifacts_configuration_builder_with_all_used_types = new ArtifactsConfigurationBuilder(all_used_types.ToArray(), artifacts_configuration, artifact_types, logger);
        
        
        Because of_building_with_all_used_types = () => result_configuration = artifacts_configuration_builder_with_all_used_types.Build(bounded_context_config);

        It should_return_an_instance = () => result_configuration.ShouldNotBeNull();
        
        It should_have_the_first_feature = () => result_configuration.Artifacts.ContainsKey(first_feature).ShouldBeTrue();
        It should_have_the_first_feature_which_has_one_command = () => result_configuration.Artifacts[first_feature].Commands.Count().ShouldEqual(1);
        It should_have_the_first_feature_which_has_one_event = () => result_configuration.Artifacts[first_feature].Events.Count().ShouldEqual(1);
        It should_have_the_first_feature_which_has_one_query = () => result_configuration.Artifacts[first_feature].Queries.Count().ShouldEqual(1);
        It should_have_the_first_feature_which_has_one_read_model = () => result_configuration.Artifacts[first_feature].ReadModels.Count().ShouldEqual(1);
        It should_have_the_first_feature_which_has_one_event_source = () => result_configuration.Artifacts[first_feature].EventSources.Count().ShouldEqual(1);
        It should_have_the_first_feature_which_has_one_command_with_the_correct_clr_type = () => result_configuration.Artifacts[first_feature].Commands.First().Type.TypeString.ShouldEqual(ClrType.FromType(typeof(Specs.Module.Feature.TheCommand)).TypeString);
        It should_have_the_first_feature_which_has_one_event_with_the_correct_clr_type = () => result_configuration.Artifacts[first_feature].Events.First().Type.TypeString.ShouldEqual(ClrType.FromType(typeof(Specs.Module.Feature.TheEvent)).TypeString); 
        It should_have_the_first_feature_which_has_one_query_with_the_correct_clr_type = () => result_configuration.Artifacts[first_feature].Queries.First().Type.TypeString.ShouldEqual(ClrType.FromType(typeof(Specs.Module.Feature.TheQuery)).TypeString); 
        It should_have_the_first_feature_which_has_one_read_model_with_the_correct_clr_type = () => result_configuration.Artifacts[first_feature].ReadModels.First().Type.TypeString.ShouldEqual(ClrType.FromType(typeof(Specs.Module.Feature.TheReadModel)).TypeString); 
        It should_have_the_first_feature_which_has_one_event_source_with_the_correct_clr_type = () => result_configuration.Artifacts[first_feature].EventSources.First().Type.TypeString.ShouldEqual(ClrType.FromType(typeof(Specs.Module.Feature.TheEventSource)).TypeString); 
        
        It should_have_the_second_feature = () => result_configuration.Artifacts.ContainsKey(second_feature).ShouldBeTrue();
        It should_have_the_second_feature_which_has_one_command = () => result_configuration.Artifacts[second_feature].Commands.Count().ShouldEqual(1);
        It should_have_the_second_feature_which_has_one_event = () => result_configuration.Artifacts[second_feature].Events.Count().ShouldEqual(1);
        It should_have_the_second_feature_which_has_one_query = () => result_configuration.Artifacts[second_feature].Queries.Count().ShouldEqual(1);
        It should_have_the_second_feature_which_has_one_read_model = () => result_configuration.Artifacts[second_feature].ReadModels.Count().ShouldEqual(1);
        It should_have_the_second_feature_which_has_one_event_source = () => result_configuration.Artifacts[second_feature].EventSources.Count().ShouldEqual(1);
        It should_have_the_second_feature_which_has_one_command_with_the_correct_clr_type = () => result_configuration.Artifacts[second_feature].Commands.First().Type.TypeString.ShouldEqual(ClrType.FromType(typeof(Specs.Module.Feature3.TheCommand)).TypeString);
        It should_have_the_second_feature_which_has_one_event_with_the_correct_clr_type = () => result_configuration.Artifacts[second_feature].Events.First().Type.TypeString.ShouldEqual(ClrType.FromType(typeof(Specs.Module.Feature3.TheEvent)).TypeString); 
        It should_have_the_second_feature_which_has_one_query_with_the_correct_clr_type = () => result_configuration.Artifacts[second_feature].Queries.First().Type.TypeString.ShouldEqual(ClrType.FromType(typeof(Specs.Module.Feature3.TheQuery)).TypeString); 
        It should_have_the_second_feature_which_has_one_read_model_with_the_correct_clr_type = () => result_configuration.Artifacts[second_feature].ReadModels.First().Type.TypeString.ShouldEqual(ClrType.FromType(typeof(Specs.Module.Feature3.TheReadModel)).TypeString); 
        It should_have_the_second_feature_which_has_one_event_source_with_the_correct_clr_type = () => result_configuration.Artifacts[second_feature].EventSources.First().Type.TypeString.ShouldEqual(ClrType.FromType(typeof(Specs.Module.Feature3.TheEventSource)).TypeString); 
        
    }
}