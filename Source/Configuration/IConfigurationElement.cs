/*---------------------------------------------------------------------------------------------
 *  Copyright (c) 2008-2017 doLittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using doLittle.DependencyInversion;

namespace doLittle.Configuration
{
    /// <summary>
    /// Interface for all configuration elements
    /// </summary>
    public interface IConfigurationElement
    {
        /// <summary>
        /// Initialization of the deriving ConfigurationElement instance
        /// </summary>
        void Initialize(IContainer container);
    }
}
