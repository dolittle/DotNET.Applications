// Copyright (c) Dolittle. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Dolittle.Applications.Configuration
{
    /// <summary>
    /// Represents the definition of a <see cref="Module"/>.
    /// </summary>
    public class ModuleDefinition
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ModuleDefinition"/> class.
        /// </summary>
        /// <param name="name"><see cref="ModuleName">Name</see> of the module.</param>
        /// <param name="features">Key/values of features and their definitions.</param>
        public ModuleDefinition(ModuleName name, IDictionary<Feature, FeatureDefinition> features)
        {
            Name = name;
            Features = new ReadOnlyDictionary<Feature, FeatureDefinition>(features);
        }

        /// <summary>
        /// Gets the <see cref="ModuleName">name of the module</see>.
        /// </summary>
        public ModuleName Name { get; }

        /// <summary>
        /// Gets a <see cref="IEnumerable{FeatureDefinition}">collection</see> of <see cref="FeatureDefinition"/>.
        /// </summary>
        public ReadOnlyDictionary<Feature, FeatureDefinition> Features { get; }
    }
}