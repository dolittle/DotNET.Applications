// Copyright (c) Dolittle. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;

namespace Dolittle.AspNetCore.Debugging.Middleware
{
    /// <summary>
    /// Exception that gets thrown when deserialization of an artifact given a type fails.
    /// </summary>
    public class ArtifactDeserializationFailed : Exception
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ArtifactDeserializationFailed"/> class.
        /// </summary>
        /// <param name="type">The <see cref="Type"/> of the artifact to deserialize.</param>
        /// <param name="exception">The <see cref="Exception"/> that was thrown durin deserialization.</param>
        public ArtifactDeserializationFailed(Type type, Exception exception)
            : base($"Deserialization of type {type} failed.", exception)
        {
        }
    }
}