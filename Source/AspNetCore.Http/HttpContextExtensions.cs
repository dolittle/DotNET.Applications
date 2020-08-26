// Copyright (c) Dolittle. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System.Buffers;
using System.IO.Pipelines;
using System.Text;
using System.Threading.Tasks;
using Dolittle.AspNetCore.Http;
using Dolittle.Serialization.Json;
using Microsoft.Extensions.DependencyInjection;

namespace Microsoft.AspNetCore.Http
{
    /// <summary>
    /// Provides extension methods for the <see cref="HttpContext" /> to read the <see cref="HttpRequest.Body" />.
    /// </summary>
    public static class HttpContextExtensions
    {
        /// <summary>
        /// Deserializes the <see cref="HttpRequest.Body" /> as a <typeparamref name="T" />.
        /// </summary>
        /// <param name="context">The <see cref="HttpContext" />.</param>
        /// <typeparam name="T">The type to deserialize to.</typeparam>
        /// <returns>A task that, when resolved, returns the <see cref="HttpRequest.Body" /> as a <typeparamref name="T" />.</returns>
        public static async Task<T> RequestBodyFromJson<T>(this HttpContext context)
        {
            if (context.Request.ContentType != "application/json") throw new RequestContentTypeIsNotJson(context.Request.ContentType);
            var serializer = context.RequestServices.GetRequiredService<ISerializer>();
            var body = await context.RequestBodyAsString().ConfigureAwait(false);
            return serializer.FromJson<T>(body);
        }

        /// <summary>
        /// Writes a response with the given result and status code.
        /// </summary>
        /// <typeparam name="T">The type of the result.</typeparam>
        /// <param name="context">The <see cref="HttpContext" />.</param>
        /// <param name="statusCode">The HTTP status code.</param>
        /// <param name="result">The result.</param>
        /// <param name="options">Optional <see cref="ISerializationOptions"/> to configure the JSON serialization.</param>
        /// <returns>A task that represents the asynchronous operation.</returns>
        public static Task RespondWithStatusCodeAndResult<T>(this HttpContext context, int statusCode, T result, ISerializationOptions options = null)
        {
            var serializer = context.RequestServices.GetRequiredService<ISerializer>();
            context.Response.StatusCode = statusCode;
            context.Response.ContentType = "application/json";
            return context.Response.WriteAsync(serializer.ToJson(result, options));
        }

        /// <summary>
        /// Reads the <see cref="HttpRequest.Body" /> as a <see cref="string" />.
        /// </summary>
        /// <param name="context">The <see cref="HttpContext" />.</param>
        /// <returns>A task that, when resolved, returns the <see cref="HttpRequest.Body" /> as a <see cref="string" />.</returns>
        public static Task<string> RequestBodyAsString(this HttpContext context) =>
            ReadAsString(context.Request.BodyReader);

        static async Task<string> ReadAsString(PipeReader reader)
        {
            ReadResult result;
            while (true)
            {
                result = await reader.ReadAsync().ConfigureAwait(false);
                if (result.IsCompleted) break;
                reader.AdvanceTo(result.Buffer.Start, result.Buffer.End);
            }

            var body = Encoding.UTF8.GetString(result.Buffer.ToArray());
            reader.AdvanceTo(result.Buffer.End);
            return body;
        }
    }
}