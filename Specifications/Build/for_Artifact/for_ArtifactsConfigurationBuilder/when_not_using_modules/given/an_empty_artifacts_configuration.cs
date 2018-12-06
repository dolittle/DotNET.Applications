/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/

using System.Collections.Generic;
using Dolittle.Applications;
using Dolittle.Artifacts.Configuration;
using Machine.Specifications;

namespace Dolittle.Build.Artifact.for_Artifact.for_ArtifactConfigurationBuilder.when_not_using_modules.given
{
    public class an_empty_artifacts_configuration : given.a_bounded_context_config
    {
        protected readonly static DolittleArtifactTypes artifact_types = new DolittleArtifactTypes();
        protected static ArtifactsConfiguration artifacts_configuration;

        Establish context = () => 
        {
            artifacts_configuration = new ArtifactsConfiguration(new Dictionary<Feature,ArtifactsByTypeDefinition>());
        };
    }
}