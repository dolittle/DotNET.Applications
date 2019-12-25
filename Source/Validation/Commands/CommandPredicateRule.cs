// Copyright (c) Dolittle. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Reflection;
using FluentValidation;
using FluentValidation.Internal;
using FluentValidation.Results;
using FluentValidation.Validators;

namespace Dolittle.Commands.Validation
{
    /// <summary>
    /// Represents the rule for a predicate in the form of a Func that will be called for validation.
    /// </summary>
    /// <typeparam name="T">Type of command the validation applies to.</typeparam>
    public class CommandPredicateRule<T> : PropertyRule
        where T : ICommand
    {
        static readonly MemberInfo IdProperty = typeof(IHiddenCommand).GetTypeInfo().GetProperty("Id");
        static readonly Func<object, object> IdFunc;
        static readonly Expression<Func<IHiddenCommand, Guid>> IdFuncExpression;
        readonly Func<T, bool> _validateFor;

        static CommandPredicateRule()
        {
            Func<IHiddenCommand, Guid> idFunc = cmd => cmd.Id;
            IdFuncExpression = (IHiddenCommand cmd) => cmd.Id;

            IdFunc = idFunc.CoerceToNonGeneric();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="CommandPredicateRule{T}"/> class.
        /// </summary>
        /// <param name="validateFor"><see cref="Func{T,TR}"/> that will be called for validation.</param>
        public CommandPredicateRule(Func<T, bool> validateFor)
            : base(IdProperty, IdFunc, IdFuncExpression, () => CascadeMode.StopOnFirstFailure, typeof(T), typeof(T))
        {
            _validateFor = validateFor;
            AddValidator(new PredicateValidator((o, p, c) => true));
        }

        /// <summary>
        /// Defines a command used internally.
        /// </summary>
        interface IHiddenCommand
        {
            /// <summary>
            /// Gets or sets the identifier.
            /// </summary>
            Guid Id { get; set; }
        }

        /// <summary>
        /// Create a <see cref="CommandPredicateRule{T}"/> from a <see cref="Func{T, TR}"/> to use for validation.
        /// </summary>
        /// <param name="validateFor"><see cref="Func{T, TR}"/> to use for validation.</param>
        /// <returns>A <see cref="CommandPredicateRule{T}"/>.</returns>
        public static CommandPredicateRule<T> Create(Func<T, bool> validateFor)
        {
            return new CommandPredicateRule<T>(validateFor);
        }

        /// <inheritdoc/>
        public override IEnumerable<ValidationFailure> Validate(ValidationContext context)
        {
            if (!_validateFor((T)context.InstanceToValidate))
            {
                return new[]
                {
                    new ValidationFailure(string.Empty, CurrentValidator.ErrorMessageSource.GetString(null))
                };
            }

            return Array.Empty<ValidationFailure>();
        }
    }
}
