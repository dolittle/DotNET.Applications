using System;
using Dolittle.Applications.Configuration;

namespace Dolittle.Build.Artifact
{
    /// <summary>
    /// Exception that gets thrown when no <see cref="FeatureDefinition"/> matching the artifact's namespace is found in the <see cref="BoundedContextConfiguration"/> topology
    /// </summary>
    public class NonMatchingArtifact : Exception
    {
        /// <summary>
        /// Instantiates an instance of <see cref="NonMatchingArtifact"/>
        /// </summary>
        /// <returns></returns>
        public NonMatchingArtifact() : base("Artifacts that did no match any of the Bounded Context Configuration's topology were found")
        {}
    }
}