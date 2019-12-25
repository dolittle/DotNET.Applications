// Copyright (c) Dolittle. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace Dolittle.Build.Topology
{
    /// <summary>
    /// Defines the configuration manager for <see cref="Topology"/>.
    /// </summary>
    public interface ITopologyConfigurationManager
    {
        /// <summary>
        /// Load the <see cref="Topology"/>.
        /// </summary>
        /// <returns>The loaded <see cref="Topology"/>.</returns>
        Applications.Configuration.Topology Load();

        /// <summary>
        /// Save the <see cref="Topology"/>.
        /// </summary>
        /// <param name="configuration"><see cref="Topology"/> to save.</param>
        void Save(Applications.Configuration.Topology configuration);
    }
}