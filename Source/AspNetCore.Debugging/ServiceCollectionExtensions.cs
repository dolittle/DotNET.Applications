// Copyright (c) Dolittle. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
using System;
using Dolittle.AspNetCore.Generators.Documents;
using Dolittle.AspNetCore.Generators.Schemas;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Swashbuckle.AspNetCore.Swagger;
using Swashbuckle.AspNetCore.SwaggerGen;
using Swashbuckle.AspNetCore.SwaggerUI;

namespace Dolittle.AspNetCore.Debugging
{
    /// <summary>
    /// <see cref="IServiceCollection"/> extensions for adding the Dolittle Swagger debugging tools.
    /// </summary>
    public static class ServiceCollectionExtensions
    {
        /// <summary>
        /// Adds the Dolittle Swagger document generators.
        /// </summary>
        /// <param name="services"><see cref="IServiceCollection"/> to add to.</param>
        /// <param name="setupAction">Optional callback for configuring <see cref="DebuggingOptions"/>.</param>
        /// <param name="setupSwaggerAction">Optional callback for configuring <see cref="SwaggerGenOptions"/>.</param>
        /// <returns><see cref="IServiceCollection"/>.</returns>
        public static IServiceCollection AddDolittleSwagger(
            this IServiceCollection services,
            Action<DebuggingOptions> setupAction = null,
            Action<SwaggerGenOptions> setupSwaggerAction = null)
        {
            if (setupAction != null) services.Configure(setupAction);
            services.AddMvc();
            services.AddSwaggerGen(setupSwaggerAction);
            services.AddTransient<ISwaggerProvider, DebuggingHandlerDocumentProvider>();
            services.AddTransient<ISchemaGenerator, SchemaGenerator>();
            services.AddTransient<IPostConfigureOptions<SwaggerUIOptions>, PostConfigureDebuggingHandlerDocumentsUI>();
            return services;
        }
    }
}