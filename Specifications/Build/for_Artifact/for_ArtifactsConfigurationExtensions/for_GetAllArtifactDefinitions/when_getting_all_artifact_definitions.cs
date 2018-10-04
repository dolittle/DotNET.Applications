/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using System.Collections.Generic;
using System.Linq;
using Dolittle.Artifacts.Configuration;
using Machine.Specifications;
namespace Dolittle.Build.Artifact.for_Artifact.for_ArtifactsConfigurationExtensions.for_GetAllArtifactDefinitions
{
    public class when_getting_all_artifact_definitions: given.an_artifact_configuration_with_two_features
    {
        static IEnumerable<ArtifactDefinition> result_artifact_definitions;

        Because of = () =>result_artifact_definitions = artifact_configuration.GetAllArtifactDefinitions();

        It should_have_the_same_amount_of_artifacts = () => result_artifact_definitions.Count().ShouldEqual(all_artifact_definitions.Count());
        It should_have_all_the_artifact_definitions = () => result_artifact_definitions.ShouldContain(all_artifact_definitions);

           
    }
}