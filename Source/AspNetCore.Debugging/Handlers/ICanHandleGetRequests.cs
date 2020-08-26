// Copyright (c) Dolittle. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace Dolittle.AspNetCore.Debugging.Handlers
{
    /// <summary>
    /// The interface to implement for a <see cref="IDebuggingHandler"/> to handle GET requests.
    /// </summary>
    /// <typeparam name="T">The type of artifacts to handle.</typeparam>
    public interface ICanHandleGetRequests<T>
        where T : class
    {
        /// <summary>
        /// The method that gets called when a GET request is recieved for an <see cref="IDebuggingHandler"/>.
        /// </summary>
        /// <param name="context">The <see cref="HttpContext"/> of the GET request.</param>
        /// <param name="artifact">The artifact contained in the GET request.</param>
        /// <returns>A <see cref="Task"/> representing the result of the asynchronous operation.</returns>
        Task HandleGetRequest(HttpContext context, T artifact);
    }
}