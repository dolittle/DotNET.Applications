/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using Dolittle.Artifacts.Configuration;

namespace Dolittle.Build.Artifacts
{
    /// <summary>
    /// Defines a system for working with <see cref="ArtifactsConfiguration"/>
    /// </summary>
    public interface IArtifactsConfigurationManager
    {
        /// <summary>
        /// Load a <see cref="ArtifactsConfiguration"/>
        /// </summary>
        /// <returns><see cref="ArtifactsConfiguration"/> loaded</returns>
        ArtifactsConfiguration  Load();

        /// <summary>
        /// Puts a <see cref="ArtifactsConfiguration"/>
        /// </summary>
        /// <param name="configuration"><see cref="ArtifactsConfiguration"/> to save</param>
        void Save(ArtifactsConfiguration configuration);
    }
}