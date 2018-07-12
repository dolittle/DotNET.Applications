using System;
using Dolittle.Artifacts;

namespace Dolittle.Applications
{
    /// <summary>
    /// The exception that gets thrown when there isn't defined a class that resolve <see cref="IApplicationArtifactIdentifier"/> of a specified <see cref="IArtifactType"/>
    /// </summary>
    public class CouldNotFindResolver : ArgumentException
    {
        /// <summary>
        /// Instantiates the <see cref="CouldNotFindResolver"/> exception
        /// </summary>
        /// <param name="typeIdentifier"></param>
        public CouldNotFindResolver(string typeIdentifier)
            : base($"Could not find an instance of {typeof(ICanResolveApplicationArtifacts).AssemblyQualifiedName} that can resolve an {typeof(IApplicationArtifactIdentifier)} with {typeof(IArtifactType).AssemblyQualifiedName} with Identifier: {typeIdentifier}")
        {
        }
    }
}