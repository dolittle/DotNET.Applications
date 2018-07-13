using System;

namespace Dolittle.Applications
{
    /// <summary>
    /// 
    /// </summary>
    public class CouldNotResolveApplicationArtifactIdentifier : Exception
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="aai"></param>
        public CouldNotResolveApplicationArtifactIdentifier(ApplicationArtifactIdentifier aai, Type type)
            : base($"Trying to resolve a {typeof(IApplicationArtifactIdentifier).AssemblyQualifiedName} to a {type.AssemblyQualifiedName} "+ 
            $"but cannot match {aai.ToString()} to any {typeof(IApplicationArtifactIdentifier).AssemblyQualifiedName} of a {type.AssemblyQualifiedName} ")
            {

            }
    }
}