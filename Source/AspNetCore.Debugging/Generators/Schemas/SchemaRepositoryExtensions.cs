// Copyright (c) Dolittle. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace Dolittle.AspNetCore.Generators.Schemas
{
    /// <summary>
    /// <see cref="SchemaRepository"/> extensions.
    /// </summary>
    public static class SchemaRepositoryExtensions
    {
        /// <summary>
        /// Populates the a <see cref="SchemaRepository"/> with schemas from an <see cref="OpenApiDocument"/>.
        /// </summary>
        /// <param name="repository">The <see cref="SchemaRepository"/> to populate.</param>
        /// <param name="document">The <see cref="OpenApiDocument"/> with schemas.</param>
        public static void PopulateWithDocumentSchemas(this SchemaRepository repository, OpenApiDocument document)
        {
            var schemas = document?.Components?.Schemas;
            if (schemas != null)
            {
                foreach ((var key, var schema) in schemas)
                {
                    repository.Schemas.Add(key, schema);
                }
            }
        }
    }
}