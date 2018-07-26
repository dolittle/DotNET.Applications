/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using System.Collections.Generic;

namespace Dolittle.Applications.Configuration
{
    /// <summary>
    /// Represents the definition of a <see cref="Feature"/>
    /// </summary>
    public class FeatureDefinition 
    {
        /// <summary>
        /// Gets or sets the <see cref="Feature"/>
        /// </summary>
        public Feature Feature { get; set; }

        /// <summary>
        /// Gets or sets the <see cref="FeatureName">name of the feature</see>
        /// </summary>
        public FeatureName Name { get; set; }

        /// <summary>
        /// Gets or sets the <see cref="IEnumerable{FeatureDefinition}">collection of child</see> <see cref="FeatureDefinition">features</see>
        /// </summary>
        /// <value></value>
        public IEnumerable<FeatureDefinition>   SubFeatures { get; set; }
    }
}