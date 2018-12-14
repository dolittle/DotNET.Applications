/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Dolittle.Applications.Configuration
{
    /// <summary>
    /// Represents the definition of a <see cref="Module"/>
    /// </summary>
    public class ModuleDefinition
    {
        /// <summary>
        /// Initializes a new instance of <see cref="ModuleDefinition"/>
        /// </summary>
        /// <param name="name"><see cref="ModuleName">Name</see> of the module</param>
        /// <param name="features">Key/values of features and their definitions</param>
        public ModuleDefinition(ModuleName name, IDictionary<Feature, FeatureDefinition> features)
        {
            Name = name;
            Features = new ReadOnlyDictionary<Feature, FeatureDefinition>(features);
        }

        /// <summary>
        /// Gets or sets the <see cref="ModuleName">name of the module</see>
        /// </summary>
        public ModuleName Name { get; }

        /// <summary>
        /// Gets or sets a <see cref="IEnumerable{FeatureDefinition}">collection</see> of <see cref="FeatureDefinition"/>
        /// </summary>
        public ReadOnlyDictionary<Feature, FeatureDefinition> Features { get; }
    }
}