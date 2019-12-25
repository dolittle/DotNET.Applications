// Copyright (c) Dolittle. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using FluentValidation.Internal;
using FluentValidation.Validators;

namespace Dolittle.Validation.MetaData
{
    /// <summary>
    /// Provides extensions for formatting messages from property validator.
    /// </summary>
    public static class PropertyValidatorMessageFormattingExtensions
    {
        /// <summary>
        /// Get a error message for a property.
        /// </summary>
        /// <param name="propertyValidator">PropertyValidator to get for.</param>
        /// <param name="propertyName">Name of propery.</param>
        /// <returns>Formatted string.</returns>
        public static string GetErrorMessageFor(this IPropertyValidator propertyValidator, string propertyName)
        {
            var formatter = new MessageFormatter().AppendPropertyName(propertyName);
            return formatter.BuildMessage(propertyValidator.ErrorMessageSource.GetString(null));
        }
    }
}
