// Copyright (c) Dolittle. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using System.Collections.Generic;
using Dolittle.AspNetCore.Debugging.Handlers;
using Dolittle.Reflection;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace Dolittle.AspNetCore.Generators.Documents
{
    /// <summary>
    /// An implementation of <see cref="IDebuggingHandlerDocumentGenerator"/>.
    /// </summary>
    public class DebuggingHandlerDocumentGenerator : IDebuggingHandlerDocumentGenerator
    {
        readonly ISchemaGenerator _schemaGenerator;

        /// <summary>
        /// Initializes a new instance of the <see cref="DebuggingHandlerDocumentGenerator"/> class.
        /// </summary>
        /// <param name="schemaGenerator">The <see cref="ISchemaGenerator"/> used to generate schemas.</param>
        public DebuggingHandlerDocumentGenerator(ISchemaGenerator schemaGenerator)
        {
            _schemaGenerator = schemaGenerator;
        }

        /// <inheritdoc/>
        public OpenApiDocument GenerateFor(IDebuggingHandler handler)
        {
            var repository = new SchemaRepository();
            var paths = new OpenApiPaths();
            GeneratePaths(paths, handler, repository);

            return new OpenApiDocument
            {
                Info = new OpenApiInfo
                {
                    Title = handler.Title,
                },
                Paths = paths,
                Components = new OpenApiComponents
                {
                    Schemas = repository.Schemas,
                },
            };
        }

        void GeneratePaths(OpenApiPaths paths, IDebuggingHandler handler, SchemaRepository repository)
        {
            foreach ((var path, var artifact) in handler.Artifacts)
            {
                var item = new OpenApiPathItem();

                AddGetOperation(handler, item, GenerateOperation(handler, artifact, repository));
                AddPostOperation(handler, item, GenerateOperation(handler, artifact, repository));

                var tag = path.ToString().Contains('/', StringComparison.InvariantCultureIgnoreCase) ? path.ToString().Split('/')[1] : path.ToString();
                foreach ((_, var operation) in item.Operations)
                {
                    operation.Tags = new[] { new OpenApiTag { Name = tag } };
                }

                paths.Add(path, item);
            }
        }

        void AddGetOperation(IDebuggingHandler handler, OpenApiPathItem item, OpenApiOperation operation)
        {
            if (handler.GetType().ImplementsOpenGeneric(typeof(ICanHandleGetRequests<>)))
            {
                item.AddOperation(OperationType.Get, operation);
            }
        }

        void AddPostOperation(IDebuggingHandler handler, OpenApiPathItem item, OpenApiOperation operation)
        {
            if (handler.GetType().ImplementsOpenGeneric(typeof(ICanHandlePostRequests<>)))
            {
                item.AddOperation(OperationType.Post, operation);
            }
        }

        OpenApiOperation GenerateOperation(IDebuggingHandler handler, Type artifact, SchemaRepository repository)
        {
            return new OpenApiOperation
            {
                RequestBody = artifact.GetProperties().Length != 0 ? new OpenApiRequestBody
                {
                    Required = true,
                    Content = GenerateContentType("application/json", artifact, repository)
                }
                : null,
                Responses = GenerateResponses(handler, repository)
            };
        }

        IDictionary<string, OpenApiMediaType> GenerateContentType(string mimeType, Type type, SchemaRepository repository)
        {
            return new Dictionary<string, OpenApiMediaType>
            {
                [mimeType] = new OpenApiMediaType { Schema = _schemaGenerator.GenerateSchema(type, repository) }
            };
        }

        OpenApiResponses GenerateResponses(IDebuggingHandler handler, SchemaRepository repository)
        {
            var responses = new OpenApiResponses();
            foreach ((var statusCode, var description) in handler.Responses)
            {
                responses.Add($"{statusCode}", new OpenApiResponse
                {
                    Description = description,
                    Content = GenerateContentType("text/plain", typeof(string), repository),
                });
            }

            return responses;
        }
    }
}
