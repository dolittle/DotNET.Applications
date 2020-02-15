// Copyright (c) Dolittle. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using System.Collections.Generic;
using System.Linq;
using Dolittle.Validation;
using FluentValidation;

namespace Dolittle.Commands.Validation
{
    /// <summary>
    /// Base class to inherit from for basic input validation of a command.
    /// </summary>
    /// <typeparam name="T">Type of <see cref="ICommand"/>.</typeparam>
    public abstract class CommandInputValidatorFor<T> : InputValidator<T>, ICanValidate<T>, ICommandInputValidator
        where T : class, ICommand
    {
        /// <inheritdoc/>
        public virtual IEnumerable<ValidationResult> ValidateFor(T command)
        {
            var result = Validate(command as T);
            return BuildValidationResults(result);
        }

        /// <summary>
        /// Validates that the object is in a valid state.
        /// </summary>
        /// <param name="command">The target to validate.</param>
        /// <param name="ruleSet">Which ruleset to validate.</param>
        /// <returns>A collection of ValidationResults. An empty collection indicates a valid command.</returns>
        public virtual IEnumerable<ValidationResult> ValidateFor(T command, string ruleSet)
        {
            var result = (this as IValidator<T>).Validate(command as T, ruleSet: ruleSet);
            return BuildValidationResults(result);
        }

        /// <inheritdoc/>
        IEnumerable<ValidationResult> ICanValidate.ValidateFor(object target)
        {
            return ValidateFor((T)target);
        }

        static IEnumerable<ValidationResult> BuildValidationResults(global::FluentValidation.Results.ValidationResult result)
        {
            return result.Errors.Select(error =>
            {
                // TODO: Due to a problem with property names being wrong when a concepts input validator is involved, we need to do this. See #494 for more details on what needs to be done!
                var propertyName = error.PropertyName;
                if (propertyName.EndsWith(".", StringComparison.InvariantCulture)) propertyName = propertyName.Substring(0, propertyName.Length - 1);

                return new ValidationResult(error.ErrorMessage, new[] { propertyName });
            });
        }
    }
}