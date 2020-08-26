// Copyright (c) Dolittle. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace Dolittle.AspNetCore.Debugging
{
    /// <summary>
    /// <see cref="HttpContext"/> extensions for responding with text.
    /// </summary>
    public static class HttpContextExtensions
    {
        /// <summary>
        /// Responds with HTTP status 200 and text.
        /// </summary>
        /// <param name="context">The <see cref="HttpContext"/> that holds the response.</param>
        /// <param name="text">The text to respond with.</param>
        /// <returns>A <see cref="Task"/> representing the result of the asynchronous operation.</returns>
        public static async Task RespondWithOk(this HttpContext context, string text)
        {
            await RespondWithStatusCodeAndText(context, StatusCodes.Status200OK, text).ConfigureAwait(false);
        }

        /// <summary>
        /// Responds with HTTP status 400 and reason.
        /// </summary>
        /// <param name="context">The <see cref="HttpContext"/> that holds the response.</param>
        /// <param name="reason">The reason describing why the request is bad.</param>
        /// <returns>A <see cref="Task"/> representing the result of the asynchronous operation.</returns>
        public static async Task RespondWithBadRequest(this HttpContext context, string reason)
        {
            await RespondWithStatusCodeAndText(context, StatusCodes.Status400BadRequest, $"Bad request, {reason}.").ConfigureAwait(false);
        }

        /// <summary>
        /// Responds with HTTP status 404 and resource not found text.
        /// </summary>
        /// <param name="context">The <see cref="HttpContext"/> that holds the response.</param>
        /// <param name="resource">The resource that was not found.</param>
        /// <returns>A <see cref="Task"/> representing the result of the asynchronous operation.</returns>
        public static async Task RespondWithNotFound(this HttpContext context, string resource)
        {
            await RespondWithStatusCodeAndText(context, StatusCodes.Status404NotFound, $"{resource} not found.").ConfigureAwait(false);
        }

        /// <summary>
        /// Respons with HTTP status 500 and an exception error.
        /// </summary>
        /// <param name="context">The <see cref="HttpContext"/> that holds the response.</param>
        /// <param name="error">The error message.</param>
        /// <returns>A <see cref="Task"/> representing the result of the asynchronous operation.</returns>
        public static async Task RespondWithError(this HttpContext context, string error)
        {
            await RespondWithStatusCodeAndText(context, StatusCodes.Status500InternalServerError, error).ConfigureAwait(false);
        }

        /// <summary>
        /// Respons with HTTP status 500 and an exception.
        /// </summary>
        /// <param name="context">The <see cref="HttpContext"/> that holds the response.</param>
        /// <param name="exception">The <see cref="Exception"/> that was thrown.</param>
        /// <returns>A <see cref="Task"/> representing the result of the asynchronous operation.</returns>
        public static async Task RespondWithException(this HttpContext context, Exception exception)
        {
            var innermostException = exception;
            while (innermostException.InnerException != null) innermostException = innermostException.InnerException;

            var message = exception.Message;
            if (innermostException != exception) message = $"{message}\nInner exception: {innermostException.Message}";

            await RespondWithError(context, message).ConfigureAwait(false);
        }

        static async Task RespondWithStatusCodeAndText(HttpContext context, int statusCode, string text)
        {
            context.Response.StatusCode = statusCode;
            context.Response.ContentType = "text/plain";
            await context.Response.WriteAsync(text).ConfigureAwait(false);
        }
    }
}