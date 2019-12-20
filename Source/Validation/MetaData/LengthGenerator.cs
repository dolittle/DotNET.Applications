// Copyright (c) Dolittle. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using System.Collections.Generic;
using FluentValidation.Validators;

namespace Dolittle.Validation.MetaData
{
    /// <summary>
    /// Represents the generater that can generate a <see cref="Length"/> rule from
    /// an <see cref="ILengthValidator"/>.
    /// </summary>
    public class LengthGenerator : ICanGenerateRule
    {
        /// <inheritdoc/>
        public IEnumerable<Type> From => new[] { typeof(ILengthValidator), typeof(LengthValidator) };

        /// <inheritdoc/>
        public Rule GeneratorFrom(string propertyName, IPropertyValidator propertyValidator)
        {
            return new Length
            {
                Min = ((ILengthValidator)propertyValidator).Min,
                Max = ((ILengthValidator)propertyValidator).Max,
                Message = propertyValidator.GetErrorMessageFor(propertyName)
            };
        }
    }
}