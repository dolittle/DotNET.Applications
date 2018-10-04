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
    public class when_validating_an_artifacts_configuration_with_too_few_types : given.an_artifacts_configuration
    {
        static readonly Type[] too_few_types = new []
        {
            typeof(Specs.Feature.TheCommand), typeof(Specs.Feature.TheEvent),
        };
        static Exception exception_when_validating_with_too_few_types; 
        Because of_validating_configuration_with_too_few_types = () => 
            exception_when_validating_with_too_few_types = Catch.Exception(() => artifacts_configuration.ValidateArtifacts(bounded_context_config, too_few_types, logger));
        
        It should_throw_an_exception = () => exception_when_validating_with_too_few_types.ShouldNotBeNull();
        It should_throw_ArtifactNoLongerInStructure = () => exception_when_validating_with_too_few_types.ShouldBeOfExactType(typeof(ArtifactNoLongerInStructure));
    }
}