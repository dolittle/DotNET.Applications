// Copyright (c) Dolittle. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System.Collections.Generic;
using System.Collections.ObjectModel;
using Dolittle.Configuration;

namespace Dolittle.Applications.Configuration
{
    /// <summary>
    /// Represents the topology of the current <see cref="Microservice">bounded context</see>.
    /// </summary>
    public class Topology : IConfigurationObject
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Topology"/> class.
        /// </summary>
        /// <param name="modules">Collection of <see cref="ModuleDefinition">modules</see>.</param>
        /// <param name="features">Collection of <see cref="FeatureDefinition">features</see>.</param>
        public Topology(
            IDictionary<Module, ModuleDefinition> modules,
            IDictionary<Feature, FeatureDefinition> features)
        {
            Modules = new ReadOnlyDictionary<Module, ModuleDefinition>(modules);
            Features = new ReadOnlyDictionary<Feature, FeatureDefinition>(features);
        }

        /// <summary>
        /// Gets a <see cref="IEnumerable{ModuleDefinition}">collection</see> of <see cref="ModuleDefinition"/>.
        /// </summary>
        public ReadOnlyDictionary<Module, ModuleDefinition> Modules { get; }

        /// <summary>
        /// Gets the <see cref="IEnumerable{FeatureDefinition}">features</see> that exists in the root.
        /// </summary>
        public ReadOnlyDictionary<Feature, FeatureDefinition> Features { get; }
    }
}