// Copyright (c) Dolittle. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using Microsoft.AspNetCore.Http;

namespace Dolittle.AspNetCore.Http
{
    /// <summary>
    /// Exception that gets thrown when the <see cref="HttpRequest.ContentType" /> is not "application/json".
    /// </summary>
    public class RequestContentTypeIsNotJson : Exception
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="RequestContentTypeIsNotJson"/> class.
        /// </summary>
        /// <param name="contentType">The <see cref="HttpRequest.ContentType" />.</param>
        public RequestContentTypeIsNotJson(string contentType)
            : base($"The content type of the {typeof(HttpRequest)} was not \"application/json\" it was \"{contentType}\"")
        {
        }
    }
}