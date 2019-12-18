// Copyright (c) Dolittle. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;

namespace Dolittle.Build.Artifacts
{
    /// <summary>
    /// Exception that gets thrown when an artifact is no longer in the structure.
    /// </summary>
    public class ArtifactNoLongerInStructure : Exception
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ArtifactNoLongerInStructure"/> class.
        /// </summary>
        public ArtifactNoLongerInStructure()
            : base("Found artifacts that doesn't exist anymore. Since we have not formalized artifact migration yet the build has to fail")
        {
        }
    }
}