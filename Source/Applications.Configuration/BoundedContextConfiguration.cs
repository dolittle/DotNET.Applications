/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/

namespace Dolittle.Applications.Configuration
{
    /// <summary>
    /// Represents the definition of a <see cref="BoundedContext"/> for configuration
    /// </summary>
    public class BoundedContextConfiguration
    {
        /// <summary>
        /// Gets or sets the <see cref="Application"/>
        /// </summary>
        public Application Application { get; set; }

        /// <summary>
        /// Gets or sets the <see cref="BoundedContext"/>
        /// </summary>
        public BoundedContext BoundedContext { get; set; }

        /// <summary>
        /// Gets or sets whether or not one is using <see cref="Module">modules</see>
        /// </summary>
        public bool UseModules { get; set; }

        /// <summary>
        /// Gets or sets the <see cref="TopologyConfiguration"/> for the <see cref="BoundedContext"/>
        /// </summary>
        public TopologyConfiguration Topology { get; set; }
    }
}