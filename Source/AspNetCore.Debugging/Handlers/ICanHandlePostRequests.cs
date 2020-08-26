// Copyright (c) Dolittle. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace Dolittle.AspNetCore.Debugging.Handlers
{
    /// <summary>
    /// The interface to implement for a <see cref="IDebuggingHandler"/> to handle POST requests.
    /// </summary>
    /// <typeparam name="TArtifact">The type of artifacts to handle.</typeparam>
    public interface ICanHandlePostRequests<TArtifact>
        where TArtifact : class
    {
        /// <summary>
        /// The method that gets called when a POST request is received for an <see cref="IDebuggingHandler"/>.
        /// </summary>
        /// <param name="context">The <see cref="HttpContext"/> of the POST request.</param>
        /// <param name="artifact">The artifact contained in the POST request.</param>
        /// <returns>A <see cref="Task"/> representing the result of the asynchronous operation.</returns>
        Task HandlePostRequest(HttpContext context, TArtifact artifact);
    }
}