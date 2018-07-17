using System;

namespace Dolittle.Applications
{
    /// <summary>
    /// The excption that gets thrown when an <see cref="IApplicationArtifactResolver"/> could not find a matching <see cref="IApplicationArtifactIdentifier"/>
    /// </summary>
    public class CouldNotResolveApplicationArtifactIdentifier : Exception
    {
        /// <summary>
        /// Initializes an instance of <see cref="CouldNotResolveApplicationArtifactIdentifier"/>
        /// </summary>
        /// <param name="aai">The <see cref="IApplicationArtifactIdentifier"/>that could not be found</param>
        /// <param name="artifactTypeType">The <see cref="Type"/> that the <see cref="IApplicationArtifactIdentifier"/>should have resolved to</param>
        public CouldNotResolveApplicationArtifactIdentifier(IApplicationArtifactIdentifier aai, Type artifactTypeType)
            : base($"Trying to resolve a {typeof(IApplicationArtifactIdentifier).AssemblyQualifiedName} to a {artifactTypeType.AssemblyQualifiedName} "+ 
            $"but cannot match {aai.ToString()} to any {typeof(IApplicationArtifactIdentifier).AssemblyQualifiedName} of a {artifactTypeType.AssemblyQualifiedName} ")
            {
            }
    }
}