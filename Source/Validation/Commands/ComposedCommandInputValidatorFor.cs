/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using System;
using System.Collections.Generic;
using System.Linq;
using Dolittle.Commands;
using Dolittle.Validation;
using FluentValidation;

namespace Dolittle.Commands.Validation
{
    /// <summary>
    /// Represents a command input validator that is constructed from discovered rules.
    /// </summary>
    public class ComposedCommandInputValidatorFor<T> : InputValidator<T>, ICanValidate<T>, ICommandInputValidator
        where T : class, ICommand
    {
        /// <summary>
        /// Instantiates an Instance of a <see cref="ComposedCommandInputValidatorFor{T}"/>.
        /// </summary>
        /// <param name="propertyTypesAndValidators">A collection of dynamically discovered validators to use.</param>
        public ComposedCommandInputValidatorFor(IDictionary<Type, IEnumerable<IValidator>> propertyTypesAndValidators)
        {
            foreach (var propertyType in propertyTypesAndValidators.Keys)
            {
                var ruleBuilderType = typeof(ComposedCommandRuleBuilder<>).MakeGenericType(propertyType);
                var ruleBuilder = Activator.CreateInstance(ruleBuilderType) as IComposedCommandRuleBuilder;
                ruleBuilder.AddTo(this, propertyTypesAndValidators[propertyType]);
            }
        }

#pragma warning disable 1591 // Xml Comments
        public IEnumerable<ValidationResult> ValidateFor(ICommand command)
        {
            return ValidateFor(command as T);
        }

        public virtual IEnumerable<ValidationResult> ValidateFor(T command)
        {
            var result = Validate(command);
            return result.Errors.Select(e => new ValidationResult(e.ErrorMessage, new[] { e.PropertyName }));
        }

        IEnumerable<ValidationResult> ICanValidate.ValidateFor(object target)
        {
            return ValidateFor((T)target);
        }
#pragma warning restore 1591 // Xml Comments
    }
}
