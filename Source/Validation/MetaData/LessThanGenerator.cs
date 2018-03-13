/*---------------------------------------------------------------------------------------------
 *  Copyright (c) 2008-2017 Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using System;
using Dolittle.Validation.MetaData;
using FluentValidation.Validators;

namespace Dolittle.Validation.MetaData
{
    /// <summary>
    /// Represents the generater that can generate a <see cref="LessThan"/> rule from
    /// a <see cref="LessThanValidator"/>
    /// </summary>
    public class LessThanGenerator : ICanGenerateRule
    {
#pragma warning disable 1591 // Xml Comments
        public Type[] From { get { return new[] { typeof(LessThanValidator) }; } }

        public Rule GeneratorFrom(string propertyName, IPropertyValidator propertyValidator)
        {
            return new LessThan
            {
                Value = ((LessThanValidator)propertyValidator).ValueToCompare,
                Message = propertyValidator.GetErrorMessageFor(propertyName)
            };
        }
#pragma warning restore 1591 // Xml Comments

    }
}
