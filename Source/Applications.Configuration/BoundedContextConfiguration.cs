/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/

using System.Collections.Generic;

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
        /// Gets or sets the <see cref="BoundedContextName"/> 
        /// </summary>
        public BoundedContextName BoundedContextName {get; set;}
        /// <summary>
        /// Gets or sets the <see cref="BackendConfiguration"/>
        /// </summary>
        public BackendConfiguration Backend {get; set;}
        /// <summary>
        /// Gets or sets the <see cref="InteractionLayerConfiguration"/> list
        /// </summary>
        /// <value></value>
        public IEnumerable<InteractionLayerConfiguration> Interaction {get; set;} = new InteractionLayerConfiguration[0];

    }
}