/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using System;
using System.Collections.Generic;
using System.Linq;
using Dolittle.Artifacts.Configuration;
using Machine.Specifications;

namespace Dolittle.Build.Artifact.for_Artifact.for_ArtifactsConfigurationExtensions.for_ValidateArtifacts.when_not_using_modules
{
    public class when_validating_an_artifacts_configuration_with_a_duplicate_artifact_id : given.an_ArtifactsConfiguration_with_duplicate_ids
    {
        static readonly Type[] all_used_types = new []
        {
            typeof(Specs.Feature.TheCommand), typeof(Specs.Feature.TheEvent),
            typeof(Specs.Feature.TheQuery), typeof(Specs.Feature.TheReadModel),
            typeof(Specs.Feature.TheEventSource), typeof(Specs.Feature3.TheCommand), 
            typeof(Specs.Feature3.TheEvent), typeof(Specs.Feature3.TheQuery),
            typeof(Specs.Feature3.TheReadModel), typeof(Specs.Feature3.TheEventSource),
        };
        static Exception exception_result; 
        Because of = () => 
        {
            exception_result = Catch.Exception( () => artifacts_configuration.ValidateArtifacts(bounded_context_config, all_used_types, logger));
        };
        It should_not_throw_exception_when_validating_with_all_used_types = () => exception_result.ShouldNotBeNull();
        It should_throw_DuplicateArtifact = () => exception_result.ShouldBeOfExactType(typeof(DuplicateArtifact));
    }
}