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
    /// Represents a command business validator that is constructed from discovered rules.
    /// </summary>
    /// <typeparam name="T">Type of <see cref="ICommand"/>.</typeparam>
    public class ComposedCommandBusinessValidatorFor<T> : BusinessValidator<T>, ICanValidate<T>, ICommandBusinessValidator
        where T : class, ICommand
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ComposedCommandBusinessValidatorFor{T}"/> class.
        /// Instantiates an Instance of a <see cref="ComposedCommandBusinessValidatorFor{T}"/>.
        /// </summary>
        /// <param name="propertyTypesAndValidators">A collection of dynamically discovered validators to use.</param>
        public ComposedCommandBusinessValidatorFor(IDictionary<Type, IEnumerable<IValidator>> propertyTypesAndValidators)
        {
            foreach (var propertyType in propertyTypesAndValidators.Keys)
            {
                var ruleBuilderType = typeof(ComposedCommandRuleBuilder<>).MakeGenericType(propertyType);
                var ruleBuilder = Activator.CreateInstance(ruleBuilderType) as IComposedCommandRuleBuilder;
                ruleBuilder.AddTo(this, propertyTypesAndValidators[propertyType]);
            }
        }

        /// <summary>
        /// Validates that the object is in a valid state.
        /// </summary>
        /// <param name="command">The <see cref="ICommand"/> to validate.</param>
        /// <returns>A collection of ValidationResults. An empty collection indicates a valid command.</returns>
        public IEnumerable<ValidationResult> ValidateFor(ICommand command)
        {
            return ValidateFor(command as T);
        }

        /// <inheritdoc/>
        public virtual IEnumerable<ValidationResult> ValidateFor(T command)
        {
            var result = Validate(command);
            return result.Errors.Select(e => new ValidationResult(e.ErrorMessage, new[] { e.PropertyName }));
        }

        /// <inheritdoc/>
        IEnumerable<ValidationResult> ICanValidate.ValidateFor(object target)
        {
            return ValidateFor((T)target);
        }
    }
}
