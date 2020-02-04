// Copyright (c) Dolittle. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using System.Collections.Generic;
using FluentValidation.Validators;

namespace Dolittle.Validation.MetaData
{
    /// <summary>
    /// Represents the generater that can generate a <see cref="NotNull"/> rule from
    /// an <see cref="INotNullValidator"/>.
    /// </summary>
    public class NotNullGenerator : ICanGenerateRule
    {
        /// <inheritdoc/>
        public IEnumerable<Type> From => new[] { typeof(INotNullValidator), typeof(NotNullGenerator) };

        /// <inheritdoc/>
        public Rule GeneratorFrom(string propertyName, IPropertyValidator propertyValidator)
        {
            return new NotNull
            {
                Message = propertyValidator.GetErrorMessageFor(propertyName)
            };
        }
    }
}
