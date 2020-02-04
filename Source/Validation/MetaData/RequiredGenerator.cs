// Copyright (c) Dolittle. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using System.Collections.Generic;
using FluentValidation.Validators;

namespace Dolittle.Validation.MetaData
{
    /// <summary>
    /// Represents the generater that can generate a <see cref="Required"/> rule from
    /// an <see cref="INotEmptyValidator"/>.
    /// </summary>
    public class RequiredGenerator : ICanGenerateRule
    {
        /// <inheritdoc/>
        public IEnumerable<Type> From => new[] { typeof(INotEmptyValidator), typeof(NotEmptyValidator) };

        /// <inheritdoc/>
        public Rule GeneratorFrom(string propertyName, IPropertyValidator propertyValidator)
        {
            return new Required
            {
                Message = propertyValidator.GetErrorMessageFor(propertyName)
            };
        }
    }
}
