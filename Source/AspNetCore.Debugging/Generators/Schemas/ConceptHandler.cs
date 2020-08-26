// Copyright (c) Dolittle. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using Dolittle.Concepts;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace Dolittle.AspNetCore.Generators.Schemas
{
    /// <summary>
    /// An implementation of <see cref="SchemaGeneratorHandler"/> that generates schemas for <see cref="ConceptAs{T}"/> types.
    /// </summary>
    public class ConceptHandler : SchemaGeneratorHandler
    {
        readonly SchemaGenerator _schemaGenerator;

        /// <summary>
        /// Initializes a new instance of the <see cref="ConceptHandler"/> class.
        /// </summary>
        /// <param name="schemaGenerator">The <see cref="SchemaGenerator"/> that calls this handler.</param>
        public ConceptHandler(SchemaGenerator schemaGenerator)
        {
            _schemaGenerator = schemaGenerator;
        }

        /// <inheritdoc/>
        public override bool CanCreateSchemaFor(Type type, out bool shouldBeReferenced)
        {
            shouldBeReferenced = false;
            return type.IsConcept();
        }

        /// <inheritdoc/>
        public override OpenApiSchema CreateSchema(Type type, SchemaRepository schemaRepository)
        {
            return _schemaGenerator.GenerateSchema(type.GetConceptValueType(), schemaRepository);
        }
    }
}
