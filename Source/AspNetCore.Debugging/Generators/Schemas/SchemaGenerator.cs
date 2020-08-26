// Copyright (c) Dolittle. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System.Text.Json;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace Dolittle.AspNetCore.Generators.Schemas
{
    /// <summary>
    /// An implementation of <see cref="SchemaGeneratorBase"/> that mimics the <see cref="JsonSchemaGenerator"/> and adds custom implementations of <see cref="SchemaGeneratorHandler"/>.
    /// </summary>
    public class SchemaGenerator : SchemaGeneratorBase
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SchemaGenerator"/> class.
        /// </summary>
        /// <param name="generatorOptions"><see cref="SchemaGeneratorOptions"/> used to configure the handlers.</param>
        /// <param name="jsonOptions"><see cref="JsonOptions"/> used to configure the handlers.</param>
        public SchemaGenerator(IOptions<SchemaGeneratorOptions> generatorOptions, IOptions<JsonOptions> jsonOptions)
            : base(generatorOptions.Value ?? new SchemaGeneratorOptions())
        {
            var options = generatorOptions.Value ?? new SchemaGeneratorOptions();
            var serializerOptions = jsonOptions.Value?.JsonSerializerOptions ?? new JsonSerializerOptions();

            AddHandler(new ConceptHandler(this));
            AddHandler(new FileTypeHandler());
            AddHandler(new JsonEnumHandler(options, serializerOptions));
            AddHandler(new JsonPrimitiveHandler());
            AddHandler(new JsonDictionaryHandler(this));
            AddHandler(new JsonArrayHandler(this));
            AddHandler(new JsonObjectHandler(options, serializerOptions, this));
        }
    }
}