/*---------------------------------------------------------------------------------------------
 *  Copyright (c) 2008-2017 doLittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using doLittle.DependencyInversion;
using doLittle.FluentValidation.Commands;
using doLittle.Reflection;
using doLittle.Types;
using FluentValidation;

namespace doLittle.Validation
{
    /// <summary>
    /// Represents the default <see cref="IValidatorFactory"/> implementation used for validators
    /// </summary>
    public class DefaultValidatorFactory : IValidatorFactory
    {
        ITypeFinder _typeFinder;
        IContainer _container;
        Dictionary<Type, Type> _validatorsByType;

        /// <summary>
        /// Initializes a new instance of <see cref="DefaultValidatorFactory"/>
        /// </summary>
        /// <param name="typeFinder">A <see cref="ITypeFinder"/> used for discovering validators</param>
        /// <param name="container">A <see cref="IContainer"/> to use for creating instances of the different validators</param>
        public DefaultValidatorFactory(ITypeFinder typeFinder, IContainer container)
        {
            _container = container;
            _validatorsByType = new Dictionary<Type, Type>();
            _typeFinder = typeFinder;
            Populate();
        }

#pragma warning disable 1591 // Xml Comments
        public IValidator GetValidator(Type type)
        {
            if (!_validatorsByType.ContainsKey(type))
                return null;


            var instance = _container.Get(_validatorsByType[type]) as IValidator;
            return instance;
        }

        public IValidator<T> GetValidator<T>()
        {
            return GetValidator(typeof(T)) as IValidator<T>;
        }
#pragma warning restore 1591 // Xml Comments

        void Populate()
        {
            var validatorTypes = _typeFinder.FindMultiple(typeof(IValidator)).Where(
                t =>
                    !t.HasInterface<ICommandInputValidator>() &&
                    !t.HasInterface<ICommandBusinessValidator>());
            foreach (var validatorType in validatorTypes)
            {
                var genericArguments = validatorType.GetTypeInfo().BaseType.GetTypeInfo().GetGenericArguments();
                if (genericArguments.Length == 1)
                {
                    var targetType = genericArguments[0];
                    _validatorsByType[targetType] = validatorType;
                }
            }
        }

    }
}
