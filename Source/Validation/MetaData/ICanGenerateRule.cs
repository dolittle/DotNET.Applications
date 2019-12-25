// Copyright (c) Dolittle. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using System.Collections.Generic;
using FluentValidation.Validators;

namespace Dolittle.Validation.MetaData
{
    /// <summary>
    /// Defines a system that can generate rule from a Fluent Validation property validator.
    /// </summary>
    public interface ICanGenerateRule
    {
        /// <summary>
        /// Gets types that are supported by the generator.
        /// </summary>
        IEnumerable<Type> From { get; }

        /// <summary>
        /// Generate from a specific <see cref="IPropertyValidator"/>.
        /// </summary>
        /// <param name="propertyName">Name of the property.</param>
        /// <param name="propertyValidator"><see cref="IPropertyValidator"/>.</param>
        /// <returns><see cref="Rule"/> instance.</returns>
        Rule GeneratorFrom(string propertyName, IPropertyValidator propertyValidator);
    }
}
