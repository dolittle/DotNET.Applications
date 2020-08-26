// Copyright (c) Dolittle. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using System.Threading.Tasks;
using Dolittle.AspNetCore.Debugging.Handlers;
using Dolittle.Types;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;

namespace Dolittle.AspNetCore.Debugging.Middleware
{
    /// <summary>
    /// Middleware that handles Swagger debugging operations for implemenations of <see cref="IDebuggingHandler"/>.
    /// </summary>
    public class DebuggingHandlersMiddleware
    {
        readonly RequestDelegate _next;
        readonly IOptions<DebuggingOptions> _options;
        readonly IInstancesOf<IDebuggingHandler> _handlers;
        readonly IDeserializeArtifactFromHttpRequest _deserializer;
        readonly IInvokeDebuggingHandlerMethods _invoker;

        /// <summary>
        /// Initializes a new instance of the <see cref="DebuggingHandlersMiddleware"/> class.
        /// </summary>
        /// <param name="next">The <see cref="RequestDelegate"/> used to call the next middleware.</param>
        /// <param name="options">The <see cref="DebuggingOptions"/> used to configure the middleware.</param>
        /// <param name="handlers">All implementations of <see cref="IDebuggingHandler"/>.</param>
        /// <param name="deserializer">The <see cref="IDeserializeArtifactFromHttpRequest"/> used to deserialize the artifact from the <see cref="HttpRequest"/>.</param>
        /// <param name="invoker">The <see cref="IInvokeDebuggingHandlerMethods"/> used to invoke the approprate debuggin handler method.</param>
        public DebuggingHandlersMiddleware(
            RequestDelegate next,
            IOptions<DebuggingOptions> options,
            IInstancesOf<IDebuggingHandler> handlers,
            IDeserializeArtifactFromHttpRequest deserializer,
            IInvokeDebuggingHandlerMethods invoker)
        {
            _next = next;
            _options = options;
            _handlers = handlers;
            _deserializer = deserializer;
            _invoker = invoker;
        }

        /// <summary>
        /// Middleware invocation method.
        /// </summary>
        /// <param name="context">Current <see cref="HttpContext"/>.</param>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
        public async Task InvokeAsync(HttpContext context)
        {
            if (context.Request.Path.StartsWithSegments(_options.Value.BasePath, StringComparison.InvariantCultureIgnoreCase, out var debuggingPath))
            {
                foreach (var handler in _handlers)
                {
                    if (debuggingPath.StartsWithSegments($"/{handler.Name}", StringComparison.InvariantCultureIgnoreCase, out var handlerPath))
                    {
                        if (handler.Artifacts.TryGetValue(handlerPath, out var artifactType))
                        {
                            await InvokeDebuggingHandler(context, handler, artifactType).ConfigureAwait(false);
                            return;
                        }

                        await context.RespondWithNotFound($"Artifact on path {handlerPath}").ConfigureAwait(false);
                        return;
                    }
                }

                await context.RespondWithNotFound("Debugging handler").ConfigureAwait(false);
                return;
            }

            await _next(context).ConfigureAwait(false);
        }

        async Task InvokeDebuggingHandler(HttpContext context, IDebuggingHandler handler, Type artifactType)
        {
            try
            {
                var artifact = await _deserializer.DeserializeArtifact(context.Request, artifactType).ConfigureAwait(false);
                await _invoker.InvokeDebugginHandlerMethod(context, handler, artifactType, artifact).ConfigureAwait(false);
            }
            catch (Exception exception)
            {
                await context.RespondWithException(exception).ConfigureAwait(false);
            }
        }
    }
}