/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using System;
using System.Collections.Generic;
using System.Linq;
using Dolittle.Applications;
using Dolittle.Applications.Configuration;
using Dolittle.Artifacts;
using Dolittle.Artifacts.Configuration;
using Dolittle.Build.Topology;
using Machine.Specifications;

namespace Dolittle.Build.Artifact.for_Artifact.for_ArtifactsConfigurationExtensions.for_ValidateArtifacts.when_not_using_modules.given
{
    public class all_dependencies : Dolittle.Build.given.an_ILogger
    {
        protected static readonly Feature feature1 = Guid.NewGuid();
        protected static readonly Feature feature2 = Guid.NewGuid();
        
    }
}