using System;

namespace doLittle.Artifacts
{
    /// <summary>
    /// Defines a system that is capable of mapping between <see cref="IArtifactType"/> and <see cref="Type"/>
    /// </summary>
    public interface IArtifactTypeToTypeMaps
    {
        /// <summary>
        /// Map from <see cref="Type"/> to a <see cref="IArtifactType"/>
        /// </summary>
        /// <param name="type"><see cref="Type"/> to map from</param>
        /// <returns><see cref="IArtifactType"/> mapped to</returns>
        IArtifactType Map(Type type);
        
        /// <summary>
        /// Map from <see cref="Type"/> to a <see cref="IArtifactType"/>
        /// </summary>
        /// <param name="type"><see cref="IArtifactType"/> to map from</param>
        /// <returns><see cref="Type"/> mapped to</returns>       
        Type Map(IArtifactType type);
    }
}