/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
 namespace Dolittle.Build.Topology
{
    /// <summary>
    /// Defines the configuration manager for <see cref="Topology"/>
    /// </summary>
    public interface ITopologyConfigurationManager 
    {
        /// <summary>
        /// Load the <see cref="Topology"/>
        /// </summary>
        /// <returns>The loaded <see cref="Topology"/></returns>
        Dolittle.Applications.Configuration.Topology Load();

        /// <summary>
        /// Save the <see cref="Topology"/>
        /// </summary>
        /// <param name="configuration"><see cref="Topology"/> to save</param>
        void Save(Dolittle.Applications.Configuration.Topology configuration);
    }
}