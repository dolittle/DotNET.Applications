// Copyright (c) Dolittle. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using Dolittle.AspNetCore.Debugging.Middleware;
using Microsoft.AspNetCore.Builder;
using Swashbuckle.AspNetCore.Swagger;
using Swashbuckle.AspNetCore.SwaggerUI;

namespace Dolittle.AspNetCore.Debugging
{
    /// <summary>
    /// <see cref="IApplicationBuilder"/> extensions for using the Dolittle Swagger debugging tools.
    /// </summary>
    public static class ApplicationBuilderExtensions
    {
        /// <summary>
        /// Use Dolittle Swagger Debugging tools for the given application.
        /// </summary>
        /// <param name="app"><see cref="IApplicationBuilder"/> to use Dolittle Swagger Debugging tools for.</param>
        /// <param name="swaggerUISetupAction">Callback for configuring <see cref="SwaggerUIOptions"/>.</param>
        /// <param name="swaggerSetupAction">Callback for configuring <see cref="SwaggerOptions"/>.</param>
        /// <returns><see cref="IApplicationBuilder"/>.</returns>
        public static IApplicationBuilder UseDolittleSwagger(
            this IApplicationBuilder app,
            Action<SwaggerUIOptions> swaggerUISetupAction = null,
            Action<SwaggerOptions> swaggerSetupAction = null)
        {
            app.UseSwagger(swaggerSetupAction);
            app.UseSwaggerUI(swaggerUISetupAction);
            app.UseMiddleware<DebuggingHandlersMiddleware>();
            return app;
        }
    }
}