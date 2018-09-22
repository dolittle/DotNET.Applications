/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
namespace Dolittle.Applications.Configuration
{
    /// <summary>
    /// Defines the configuration manager for <see cref="TopologyConfiguration"/>
    /// </summary>
    public interface ITopologyConfigurationManager 
    {
        /// <summary>
        /// Load the <see cref="TopologyConfiguration"/>
        /// </summary>
        /// <returns>The loaded <see cref="TopologyConfiguration"/></returns>
        TopologyConfiguration Load();

        /// <summary>
        /// Save the <see cref="TopologyConfiguration"/>
        /// </summary>
        /// <param name="configuration"><see cref="TopologyConfiguration"/> to save</param>
        void Save(TopologyConfiguration configuration);
    }
}