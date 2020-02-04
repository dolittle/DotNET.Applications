// Copyright (c) Dolittle. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using Dolittle.Artifacts;
using Dolittle.Artifacts.Configuration;

namespace Dolittle.Build
{
    /// <summary>
    /// Represents a type of artifact.
    /// </summary>
    public class ArtifactType
    {
        /// <summary>
        /// Gets or sets the type of <see cref="Artifact"/>.
        /// </summary>
        public Type Type { get; set; }

        /// <summary>
        /// Gets or sets the human friendly type name.
        /// </summary>
        public string TypeName { get; set; }

        /// <summary>
        /// Gets or sets the expression for accessing the collection of <see cref="ArtifactDefinition"/> on the configuration object.
        /// </summary>
        public Expression<Func<ArtifactsByTypeDefinition, IReadOnlyDictionary<ArtifactId, ArtifactDefinition>>> TargetPropertyExpression { get; set; }
    }
}