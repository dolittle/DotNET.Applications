/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using System.Collections.Generic;

namespace Dolittle.Applications.Configuration
{
    /// <summary>
    /// Represents the topology of an <see cref="Application">application</see> and the current <see cref="BoundedContext">bounded context</see>
    /// </summary>
    public class Topology
    {
        /// <summary>
        /// Gets or sets a <see cref="IEnumerable{ModuleDefinition}">collection</see> of <see cref="ModuleDefinition"/>
        /// </summary>
        public IEnumerable<ModuleDefinition>    Modules { get; set; } = new ModuleDefinition[0];

        /// <summary>
        /// Gets or sets the <see cref="IEnumerable{FeatureDefinition}">features</see> that exists in the root
        /// </summary>
        public IEnumerable<FeatureDefinition>   Features { get; set; } = new FeatureDefinition[0];
    }
}