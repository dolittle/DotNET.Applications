// Copyright (c) Dolittle. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using System.Collections.Generic;
using FluentValidation.Validators;

namespace Dolittle.Validation.MetaData
{
    /// <summary>
    /// Represents the generator for generating a <see cref="Regex"/> rule from a <see cref="IRegularExpressionValidator"/>.
    /// </summary>
    public class RegexGenerator : ICanGenerateRule
    {
        /// <inheritdoc/>
        public IEnumerable<Type> From => new[] { typeof(IRegularExpressionValidator), typeof(RegularExpressionValidator) };

        /// <inheritdoc/>
        public Rule GeneratorFrom(string propertyName, IPropertyValidator propertyValidator)
        {
            var rule = new Regex
            {
                Message = propertyValidator.GetErrorMessageFor(propertyName),
                Expression = ((IRegularExpressionValidator)propertyValidator).Expression
            };
            return rule;
        }
    }
}
