// Copyright (c) Dolittle. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using Dolittle.AspNetCore.Debugging;
using Dolittle.AspNetCore.Debugging.Handlers;
using Dolittle.Types;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.Swagger;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace Dolittle.AspNetCore.Generators.Documents
{
    /// <summary>
    /// An <see cref="ISwaggerProvider"/> that responds to requests for an <see cref="OpenApiDocument"/> for an <see cref="IDebuggingHandler"/>.
    /// </summary>
    public class DebuggingHandlerDocumentProvider : ISwaggerProvider
    {
        readonly IOptions<DebuggingOptions> _options;
        readonly SwaggerGenerator _swaggerGenerator;
        readonly IDebuggingHandlerDocumentGenerator _documentGenerator;
        readonly IInstancesOf<IDebuggingHandler> _handlers;
        readonly IInstancesOf<ICanModifyDebugginHandlerDocument> _modifiers;

        /// <summary>
        /// Initializes a new instance of the <see cref="DebuggingHandlerDocumentProvider"/> class.
        /// </summary>
        /// <param name="options">The <see cref="DebuggingOptions"/> used for configuration.</param>
        /// <param name="swaggerGenerator">The original <see cref="ISwaggerProvider"/> that provides documents for normal APIs.</param>
        /// <param name="documentGenerator">The <see cref="IDebuggingHandlerDocumentGenerator"/> used to generate documents for debugging handlers.</param>
        /// <param name="handlers">All implementations of <see cref="IDebuggingHandler"/>.</param>
        /// <param name="modifiers">All implementations of <see cref="ICanModifyDebugginHandlerDocument"/> that will be called to modify the document.</param>
        public DebuggingHandlerDocumentProvider(
            IOptions<DebuggingOptions> options,
            SwaggerGenerator swaggerGenerator,
            IDebuggingHandlerDocumentGenerator documentGenerator,
            IInstancesOf<IDebuggingHandler> handlers,
            IInstancesOf<ICanModifyDebugginHandlerDocument> modifiers)
        {
            _options = options;
            _swaggerGenerator = swaggerGenerator;
            _documentGenerator = documentGenerator;
            _handlers = handlers;
            _modifiers = modifiers;
        }

        /// <inheritdoc/>
        public OpenApiDocument GetSwagger(string documentName, string host = null, string basePath = null)
        {
            foreach (var handler in _handlers)
            {
                if ($"Dolittle.{handler.Name}".Equals(documentName, StringComparison.InvariantCultureIgnoreCase))
                {
                    var document = _documentGenerator.GenerateFor(handler);

                    var path = basePath == null ? PathString.Empty : (PathString)basePath;
                    var serverPath = path.Add(_options.Value.BasePath).Add($"/{handler.Name}");

                    document.Servers = new[]
                    {
                        new OpenApiServer
                        {
                            Url = $"{host}{serverPath}"
                        }
                    };

                    foreach (var modifier in _modifiers)
                    {
                        modifier.ModifyDocument(handler, document);
                    }

                    return document;
                }
            }

            return _swaggerGenerator.GetSwagger(documentName, host, basePath);
        }
    }
}