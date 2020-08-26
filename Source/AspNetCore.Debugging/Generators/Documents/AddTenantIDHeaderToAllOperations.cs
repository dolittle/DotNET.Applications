// Copyright (c) Dolittle. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System.Collections.Generic;
using Dolittle.AspNetCore.Debugging.Handlers;
using Dolittle.AspNetCore.Generators.Schemas;
using Dolittle.DependencyInversion;
using Dolittle.Tenancy;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace Dolittle.AspNetCore.Generators.Documents
{
    /// <summary>
    /// An implementation of <see cref="ICanModifyDebugginHandlerDocument"/> that adds the <see cref="TenantId"/> header to all operations.
    /// </summary>
    public class AddTenantIDHeaderToAllOperations : ICanModifyDebugginHandlerDocument
    {
        readonly FactoryFor<ISchemaGenerator> _schemaGeneratorFactory;

        /// <summary>
        /// Initializes a new instance of the <see cref="AddTenantIDHeaderToAllOperations"/> class.
        /// </summary>
        /// <param name="schemaGeneratorFactory">A factory for <see cref="ISchemaGenerator"/>.</param>
        public AddTenantIDHeaderToAllOperations(FactoryFor<ISchemaGenerator> schemaGeneratorFactory)
        {
            _schemaGeneratorFactory = schemaGeneratorFactory;
        }

        /// <inheritdoc/>
        public void ModifyDocument(IDebuggingHandler handler, OpenApiDocument document)
        {
            var schemaGenerator = _schemaGeneratorFactory();
            var repository = new SchemaRepository();
            repository.PopulateWithDocumentSchemas(document);

            foreach ((_, var item) in document.Paths)
            {
                foreach ((_, var operation) in item.Operations)
                {
                    if (operation.Parameters == null) operation.Parameters = new List<OpenApiParameter>();
                    operation.Parameters.Add(GenerateTenantIdParameter(schemaGenerator, repository));
                }
            }

            document.Components.Schemas = repository.Schemas;
        }

        OpenApiParameter GenerateTenantIdParameter(ISchemaGenerator schemaGenerator, SchemaRepository repository)
        {
            return new OpenApiParameter
            {
                Name = "Tenant-ID",
                In = ParameterLocation.Header,
                Required = true,
                Schema = schemaGenerator.GenerateSchema(typeof(TenantId), repository),
            };
        }
    }
}