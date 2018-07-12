using System;
using Dolittle.Applications;

namespace Dolittle.Commands
{
    /// <summary>
    /// The exception that gets thrown when <see cref="ApplicationArtifactResolverForCommand"/> tries to resolve a <see cref="IApplicationArtifactIdentifier"/> which it cannot find to a <see cref="ICommand"/>
    /// </summary>
    public class CommandNotFound : Exception
    {
        /// <summary>
        /// Instantiates <see cref="CommandNotFound"/> 
        /// </summary>
        /// <param name="aai"></param>
        public CommandNotFound(IApplicationArtifactIdentifier aai)
            : base($"Trying to resolve and {typeof(IApplicationArtifactIdentifier).AssemblyQualifiedName} to an {typeof(ICommand).AssemblyQualifiedName} "+ 
            $"but cannot match {aai.ToString()} with any {typeof(IApplicationArtifactIdentifier).AssemblyQualifiedName} of a {typeof(ICommand).AssemblyQualifiedName} ") 
        {}
    }
}