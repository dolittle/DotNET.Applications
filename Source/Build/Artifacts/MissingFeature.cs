// Copyright (c) Dolittle. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;

namespace Dolittle.Build.Artifacts
{
    /// <summary>
    /// Exception that gets thrown when a feature is missing.
    /// </summary>
    public class MissingFeature : Exception
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="MissingFeature"/> class.
        /// </summary>
        /// <param name="featureName">The name of the feature that is missing.</param>
        public MissingFeature(string featureName)
            : base($"Feature {featureName} was not found.")
        {
        }
    }
}