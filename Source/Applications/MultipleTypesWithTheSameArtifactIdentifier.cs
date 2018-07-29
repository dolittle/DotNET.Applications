using System;

namespace Dolittle.Applications
{
    /// <summary>
    /// The exception that gets thrown when there are multiple instances of <see cref="Type"/> that are mapped up to the same <see cref="IApplicationArtifactIdentifier"/> 
    /// </summary>
    public class MultipleTypesWithTheSameArtifactIdentifier : Exception
    {
        /// <summary>
        /// Initializes an instance of <see cref="MultipleTypesWithTheSameArtifactIdentifier"/>
        /// </summary>
        /// <param name="aai"></param>
        public MultipleTypesWithTheSameArtifactIdentifier(IApplicationArtifactIdentifier aai)
         : base($"The artifact {aai.Artifact.Name} at location {aai.Location.ToString()} is already mapped to another {typeof(Type).AssemblyQualifiedName}")
         {
         }
    }
}