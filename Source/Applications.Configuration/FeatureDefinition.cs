// Copyright (c) Dolittle. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Dolittle.Applications.Configuration
{
    /// <summary>
    /// Represents the definition of a <see cref="Feature"/>.
    /// </summary>
    public class FeatureDefinition
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="FeatureDefinition"/> class.
        /// </summary>
        /// <param name="name"><see cref="FeatureName">Name</see> of the feature.</param>
        /// <param name="subFeatures">Key/Value pairs of features and their definitions for any sub-features.</param>
        public FeatureDefinition(FeatureName name, IDictionary<Feature, FeatureDefinition> subFeatures)
        {
            Name = name;
            SubFeatures = new ReadOnlyDictionary<Feature, FeatureDefinition>(subFeatures);
        }

        /// <summary>
        /// Gets the <see cref="FeatureName">name of the feature</see>.
        /// </summary>
        public FeatureName Name { get; }

        /// <summary>
        /// Gets the <see cref="IEnumerable{FeatureDefinition}">sub features</see> that exists.
        /// </summary>
        public ReadOnlyDictionary<Feature, FeatureDefinition> SubFeatures { get; }
    }
}