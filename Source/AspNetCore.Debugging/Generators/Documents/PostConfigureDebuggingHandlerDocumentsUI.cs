// Copyright (c) Dolittle. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using Dolittle.AspNetCore.Debugging.Handlers;
using Dolittle.Types;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Options;
using Swashbuckle.AspNetCore.SwaggerUI;

namespace Dolittle.AspNetCore.Generators.Documents
{
    /// <summary>
    /// A <see cref="SwaggerUIOptions"/> post configurer that adds documents for all implementations of <see cref="IDebuggingHandler"/> to the Swagger UI.
    /// </summary>
    public class PostConfigureDebuggingHandlerDocumentsUI : IPostConfigureOptions<SwaggerUIOptions>
    {
        readonly IInstancesOf<IDebuggingHandler> _handlers;

        /// <summary>
        /// Initializes a new instance of the <see cref="PostConfigureDebuggingHandlerDocumentsUI"/> class.
        /// </summary>
        /// <param name="handlers">All implemented debugging handlers.</param>
        public PostConfigureDebuggingHandlerDocumentsUI(IInstancesOf<IDebuggingHandler> handlers)
        {
            _handlers = handlers;
        }

        /// <inheritdoc/>
        public void PostConfigure(string name, SwaggerUIOptions options)
        {
            foreach (var handler in _handlers)
            {
                options.SwaggerEndpoint($"Dolittle.{handler.Name}/swagger.json", handler.Title);
            }
        }
    }
}