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
    public class when_validating_a_correct_artifacts_configuration : given.an_artifacts_configuration
    {
        static readonly Type[] all_used_types = new []
        {
            typeof(Specs.Feature.TheCommand), typeof(Specs.Feature.TheEvent),
            typeof(Specs.Feature.TheQuery), typeof(Specs.Feature.TheReadModel),
            typeof(Specs.Feature.TheEventSource), typeof(Specs.Feature3.TheCommand), 
            typeof(Specs.Feature3.TheEvent), typeof(Specs.Feature3.TheQuery),
            typeof(Specs.Feature3.TheReadModel), typeof(Specs.Feature3.TheEventSource),
        };
        static Exception exception_when_validating_with_all_used_types; 
        Because of_validating_configuration_with_all_used_types = () => 
            exception_when_validating_with_all_used_types = Catch.Exception( () => artifacts_configuration.ValidateArtifacts(bounded_context_config, all_used_types, buildMessages));
        
        It should_not_throw_exception = () => exception_when_validating_with_all_used_types.ShouldBeNull();
    }
}