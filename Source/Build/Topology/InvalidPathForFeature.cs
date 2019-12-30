// Copyright (c) Dolittle. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;

namespace Dolittle.Build.Topology
{
    /// <summary>
    /// Exception that gets thrown when a path is invalid for a module.
    /// </summary>
    public class InvalidPathForFeature : Exception
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="InvalidPathForFeature"/> class.
        /// </summary>
        /// <param name="path">The invalid path.</param>
        public InvalidPathForFeature(string path)
            : base($"Could not get feature from path: '{path}'. Path cannot be empty, contain spaces or dashes")
        {
        }
    }
}