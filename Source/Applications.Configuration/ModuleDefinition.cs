/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using System.Collections.Generic;

namespace Dolittle.Applications.Configuration
{
    /// <summary>
    /// Represents the definition of a <see cref="Module"/>
    /// </summary>
    public class ModuleDefinition
    {
        /// <summary>
        /// Gets or sets the <see cref="Module"/>
        /// </summary>
        public Module   Module { get; set; }

        /// <summary>
        /// Gets or sets the <see cref="ModuleName">name of the module</see>
        /// </summary>
        public ModuleName Name { get; set; }

        /// <summary>
        /// Gets or sets a <see cref="IEnumerable{FeatureDefinition}">collection</see> of <see cref="FeatureDefinition"/>
        /// </summary>
        /// <value></value>
        public IEnumerable<FeatureDefinition> Features { get; set; } = new FeatureDefinition[0];
    }
}