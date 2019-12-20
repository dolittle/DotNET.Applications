// Copyright (c) Dolittle. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using System.Collections.Generic;
using FluentValidation.Validators;

namespace Dolittle.Validation.MetaData
{
    /// <summary>
    /// Represents the generater that can generate a <see cref="Email"/> rule from
    /// a <see cref="IEmailValidator"/>.
    /// </summary>
    public class EmailGenerator : ICanGenerateRule
    {
        /// <inheritdoc/>
        public IEnumerable<Type> From => new[] { typeof(IEmailValidator), typeof(EmailValidator) };

        /// <inheritdoc/>
        public Rule GeneratorFrom(string propertyName, IPropertyValidator propertyValidator)
        {
            return new Email
            {
                Message = propertyValidator.GetErrorMessageFor(propertyName)
            };
        }
    }
}
