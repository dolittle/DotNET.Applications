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
    public class when_getting_all_artifact_definitions_by_feature : given.an_ArtifactConfiguration_with_two_features
    {
        static IEnumerable<ArtifactDefinition> result_artifact_definitions_for_feature1;
        static IEnumerable<ArtifactDefinition> result_artifact_definitions_for_feature2;

        Because of = () => 
        {
            result_artifact_definitions_for_feature1 = artifact_configuration.GetAllArtifactDefinitions(feature1);

            result_artifact_definitions_for_feature2 = artifact_configuration.GetAllArtifactDefinitions(feature2);
        };
        It should_have_the_same_amount_of_artifacts_as_feature1 = () => result_artifact_definitions_for_feature1.Count().ShouldEqual(all_artifact_definitions_of_feature1.Count());
        It should_have_all_the_artifact_definitions_of_feature1 = () => result_artifact_definitions_for_feature1.ShouldContain(all_artifact_definitions_of_feature1);
        
        
        It should_have_the_same_amount_of_artifacts_as_feature2 = () => result_artifact_definitions_for_feature2.Count().ShouldEqual(all_artifact_definitions_of_feature2.Count());
        It should_have_all_the_artifact_definitions_of_feature2 = () => result_artifact_definitions_for_feature2.ShouldContain(all_artifact_definitions_of_feature2);
        
    }
}