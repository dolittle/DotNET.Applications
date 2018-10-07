/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using System;
using System.Collections.Generic;
using System.Linq;
using Dolittle.Artifacts.Configuration;
using Machine.Specifications;

namespace Dolittle.Build.Artifact.for_Artifact.for_ArtifactsConfigurationExtensions.for_ValidateArtifacts.when_using_modules
{
    public class when_validating_an_artifacts_configuration_with_a_duplicate_artifact_id : given.an_artifacts_configuration_with_duplicate_ids
    {
        static readonly Type[] all_used_types = new []
        {
            typeof(Specs.Module.Feature.TheCommand), typeof(Specs.Module.Feature.TheEvent),
            typeof(Specs.Module.Feature.TheQuery), typeof(Specs.Module.Feature.TheReadModel),
            typeof(Specs.Module.Feature.TheEventSource), typeof(Specs.Module.Feature3.TheCommand), 
            typeof(Specs.Module.Feature3.TheEvent), typeof(Specs.Module.Feature3.TheQuery),
            typeof(Specs.Module.Feature3.TheReadModel), typeof(Specs.Module.Feature3.TheEventSource),
        };
        static Exception exception_result; 
        Because of_validating_with_duplicate_artifact_ids = () => 
            exception_result = Catch.Exception( () => artifacts_configuration.ValidateArtifacts(bounded_context_config, all_used_types, logger));
            
        It should_throw_exception = () => exception_result.ShouldNotBeNull();
        It should_throw_DuplicateArtifact = () => exception_result.ShouldBeOfExactType(typeof(DuplicateArtifact));
    }
}