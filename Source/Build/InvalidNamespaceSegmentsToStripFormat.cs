// Copyright (c) Dolittle. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;

namespace Dolittle.Build
{
    /// <summary>
    /// Exception that gets thrown when the NamespaceSegmentsToStrip format is invalid.
    /// </summary>
    public class InvalidNamespaceSegmentsToStripFormat : Exception
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="InvalidNamespaceSegmentsToStripFormat"/> class.
        /// </summary>
        /// <param name="actual">Actual string used.</param>
        public InvalidNamespaceSegmentsToStripFormat(string actual)
            : base($"Invalid 'NamespaceSegmentsToString' format of '{actual}' - the format should be: \n<NamespaceSegmentsToStrip>NamespacePrefix1=This|NamespacePrefix2=Other")
        {
        }
    }
}