/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
namespace Dolittle.Applications.Configuration
{
    /// <summary>
    /// Defines the configuration manager for <see cref="BoundedContextConfiguration"/>
    /// </summary>
    public interface IBoundedContextConfigurationManager 
    {
        /// <summary>
        /// Load <see cref="BoundedContextConfiguration"/>
        /// </summary>
        /// <returns>The loaded <see cref="BoundedContextConfiguration"/></returns>
        BoundedContextConfiguration Load();

        /// <summary>
        /// Save <see cref="BoundedContextConfiguration"/>
        /// </summary>
        /// <param name="configuration"><see cref="BoundedContextConfiguration"/> to save</param>
        void Save(BoundedContextConfiguration configuration);
    }
}