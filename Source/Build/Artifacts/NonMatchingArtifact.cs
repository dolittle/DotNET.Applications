// Copyright (c) Dolittle. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using Dolittle.Applications.Configuration;
using Dolittle.Microservice.Configuration;

namespace Dolittle.Build.Artifacts
{
    /// <summary>
    /// Exception that gets thrown when no <see cref="FeatureDefinition"/> matching the artifact's namespace is found in the <see cref="MicroserviceConfiguration"/> topology.
    /// </summary>
    public class NonMatchingArtifact : Exception
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="NonMatchingArtifact"/> class.
        /// </summary>
        public NonMatchingArtifact()
            : base("Artifacts that did no match any of the Bounded Context Configuration's topology were found")
        {
        }
    }
}