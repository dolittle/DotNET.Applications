// Copyright (c) Dolittle. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using System.Collections.Generic;
using FluentValidation.Validators;

namespace Dolittle.Validation.MetaData
{
    /// <summary>
    /// Represents the generater that can generate a <see cref="LessThan"/> rule from
    /// a <see cref="LessThanValidator"/>.
    /// </summary>
    public class LessThanGenerator : ICanGenerateRule
    {
        /// <inheritdoc/>
        public IEnumerable<Type> From => new[] { typeof(LessThanValidator) };

        /// <inheritdoc/>
        public Rule GeneratorFrom(string propertyName, IPropertyValidator propertyValidator)
        {
            return new LessThan
            {
                Value = ((LessThanValidator)propertyValidator).ValueToCompare,
                Message = propertyValidator.GetErrorMessageFor(propertyName)
            };
        }
    }
}
