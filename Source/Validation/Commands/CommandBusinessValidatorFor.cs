// Copyright (c) Dolittle. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using System.Collections.Generic;
using System.Linq;
using Dolittle.Validation;
using FluentValidation;
using FluentValidation.Internal;

namespace Dolittle.Commands.Validation
{
    /// <summary>
    /// Base class to inherit from for basic business-rule validation of a command.
    /// </summary>
    /// <typeparam name="T">Type of <see cref="ICommand"/>.</typeparam>
    public abstract class CommandBusinessValidatorFor<T> : BusinessValidator<T>, ICanValidate<T>, ICommandBusinessValidator
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

        /// <summary>
        /// Add a predicate rule based on a Func that will be called when validation occurs.
        /// </summary>
        /// <param name="validateFor"><see cref="Func{T, TR}"/> that will be called for validation.</param>
        /// <returns><see cref="IRuleBuilderOptions{T, TR}"/> that can be used to fluently configure options for the rule.</returns>
        public IRuleBuilderOptions<T, object> AddRule(Func<T, bool> validateFor)
        {
            var rule = CommandPredicateRule<T>.Create(validateFor);
            AddRule(rule);

            return new RuleBuilder<T, object>(rule);
        }

        /// <inheritdoc/>
        IEnumerable<ValidationResult> ICanValidate.ValidateFor(object target)
        {
            return ValidateFor((T)target);
        }

        static IEnumerable<ValidationResult> BuildValidationResults(global::FluentValidation.Results.ValidationResult result)
        {
            return from error in result.Errors
                   select new ValidationResult(error.ErrorMessage, new[] { error.PropertyName });
        }
    }
}