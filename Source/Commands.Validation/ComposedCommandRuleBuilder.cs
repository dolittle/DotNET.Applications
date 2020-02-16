// Copyright (c) Dolittle. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using Dolittle.Validation;
using FluentValidation;

namespace Dolittle.Commands.Validation
{
    /// <summary>
    /// Represents an implementation of <see cref="IComposedCommandRuleBuilder"/>.
    /// </summary>
    /// <typeparam name="TProperty">The type of property to discover validators of.</typeparam>
    public class ComposedCommandRuleBuilder<TProperty> : IComposedCommandRuleBuilder
    {
        /// <inheritdoc/>
        public void AddTo<TCommand>(AbstractValidator<TCommand> validator, IEnumerable<IValidator> childValidators)
        {
            if (childValidators?.Any() != true)
            {
                return;
            }

            var composedValidator = new ComposedValidator<TProperty>(childValidators);
            foreach (var property in GetPropertiesWithType<TCommand>())
            {
                var expression = BuildGetExpression<TCommand>(property);
                validator.RuleFor(expression).DynamicValidationRule(composedValidator, property.Name);
            }
        }

        static IEnumerable<PropertyInfo> GetPropertiesWithType<TCommand>()
        {
            return typeof(TCommand)
                .GetTypeInfo()
                .GetProperties(BindingFlags.Instance | BindingFlags.Public)
                .Where(p => p.PropertyType == typeof(TProperty));
        }

        static Expression<Func<TCommand, TProperty>> BuildGetExpression<TCommand>(PropertyInfo propertyInfo)
        {
            var arg = Expression.Parameter(typeof(TCommand), "x");
            Expression expr = arg;
            expr = Expression.Property(expr, propertyInfo);
            return Expression.Lambda<Func<TCommand, TProperty>>(expr, arg);
        }
    }
}
