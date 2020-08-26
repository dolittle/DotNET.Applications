// Copyright (c) Dolittle. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace Dolittle.AspNetCore.Debugging.Middleware
{
    /// <summary>
    /// Represents a system that can deserialize an artifact by type from an <see cref="HttpRequest"/>.
    /// </summary>
    public interface IDeserializeArtifactFromHttpRequest
    {
        /// <summary>
        /// Deserializes the artifact contained in the request.
        /// </summary>
        /// <param name="request">The <see cref="HttpRequest"/> that contains the artifact to deserialize.</param>
        /// <param name="type">The expected <see cref="Type"/> of the artifact to deserialize.</param>
        /// <returns>The deserialized artifact instance as a <see cref="Task{Object}"/>.</returns>
        Task<object> DeserializeArtifact(HttpRequest request, Type type);
    }
}