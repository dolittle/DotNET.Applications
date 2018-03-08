/*---------------------------------------------------------------------------------------------
 *  Copyright (c) 2008-2017 doLittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using System;
using doLittle.Validation.MetaData;
using FluentValidation.Validators;

namespace doLittle.Validation.MetaData
{
    /// <summary>
    /// Represents the generater that can generate a <see cref="Email"/> rule from
    /// a <see cref="IEmailValidator"/>
    /// </summary>
    public class EmailGenerator : ICanGenerateRule
    {
#pragma warning disable 1591 // Xml Comments
        public Type[] From { get { return new[] { typeof(IEmailValidator), typeof(EmailValidator) }; } }

        public Rule GeneratorFrom(string propertyName, IPropertyValidator propertyValidator)
        {
            var emailRule = new Email
            {
                Message = propertyValidator.GetErrorMessageFor(propertyName)
            };
            return emailRule;
        }
#pragma warning restore 1591 // Xml Comments
    }
}
