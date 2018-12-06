/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using System.Collections.Generic;
using Dolittle.Configuration;

namespace Dolittle.Applications.Configuration
{
    /// <summary>
    /// Represents the topology of the current <see cref="BoundedContext">bounded context</see>
    /// </summary>
    public class Topology : IConfigurationObject
    {
        /// <summary>
        /// Initializes a new instance of <see cref="Topology"/>
        /// </summary>
        /// <param name="modules">Collection of <see cref="ModuleDefinition">modules</see>></param>
        /// <param name="features">Collection of <see cref="FeatureDefinition">features</see></param>
        public Topology(
            IEnumerable<ModuleDefinition> modules,
            IEnumerable<FeatureDefinition> features)
        {
            Modules = modules;
            Features = features;
        }
    

        /// <summary>
        /// Gets a <see cref="IEnumerable{ModuleDefinition}">collection</see> of <see cref="ModuleDefinition"/>
        /// </summary>
        public IEnumerable<ModuleDefinition> Modules { get; }

        /// <summary>
        /// Gets the <see cref="IEnumerable{FeatureDefinition}">features</see> that exists in the root
        /// </summary>
        public IEnumerable<FeatureDefinition> Features { get; }
    }
}