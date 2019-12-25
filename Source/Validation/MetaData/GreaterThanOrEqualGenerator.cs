// Copyright (c) Dolittle. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using System.Collections.Generic;
using FluentValidation.Validators;

namespace Dolittle.Validation.MetaData
{
    /// <summary>
    /// Represents the generater that can generate a <see cref="GreaterThanOrEqual"/> rule from
    /// a <see cref="GreaterThanOrEqualValidator"/>.
    /// </summary>
    public class GreaterThanOrEqualGenerator : ICanGenerateRule
    {
        /// <inheritdoc/>
        public IEnumerable<Type> From => new[] { typeof(GreaterThanOrEqualValidator) };

        /// <inheritdoc/>
        public Rule GeneratorFrom(string propertyName, IPropertyValidator propertyValidator)
        {
            return new GreaterThanOrEqual
            {
                Value = ((GreaterThanOrEqualValidator)propertyValidator).ValueToCompare,
                Message = propertyValidator.GetErrorMessageFor(propertyName)
            };
        }
    }
}
