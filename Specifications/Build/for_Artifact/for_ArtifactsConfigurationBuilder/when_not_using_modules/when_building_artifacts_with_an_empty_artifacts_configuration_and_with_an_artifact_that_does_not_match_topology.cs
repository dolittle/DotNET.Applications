/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/

using System;
using System.Collections.Generic;
using System.Linq;
using Dolittle.Artifacts.Configuration;
using Machine.Specifications;

namespace Dolittle.Build.Artifact.for_Artifact.for_ArtifactConfigurationBuilder.when_not_using_modules
{
    public class when_building_artifacts_with_an_empty_artifacts_configuration_and_with_an_artifact_that_does_not_match_topology : given.an_empty_artifacts_configuration
    {
        static readonly IEnumerable<Type> types_with_a_non_matching_type = new []
        {
            typeof(Specs.Feature.TheCommand), typeof(Specs.Feature.TheEvent),
            typeof(Specs.Feature3.TheCommand),
            typeof(Specs.Feature.Feature2.TheEvent)
        };

        static ArtifactsConfigurationBuilder artifacts_configuration_builder_with_a_non_matching_type;
        static Exception result_exception;
        
        Establish context = () => artifacts_configuration_builder_with_a_non_matching_type = new ArtifactsConfigurationBuilder(types_with_a_non_matching_type.ToArray(), artifacts_configuration, artifact_types, buildMessages);
        
        Because of_building_with_a_type_not_matching_structure = () => result_exception = Catch.Exception( () => artifacts_configuration_builder_with_a_non_matching_type.Build(bounded_context_config));

        It should_throw_an_exception = () => result_exception.ShouldNotBeNull();
        It should_throw_NonMatchingArtifact = () => result_exception.ShouldBeOfExactType(typeof(NonMatchingArtifact));
    }
}