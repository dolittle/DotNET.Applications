/*---------------------------------------------------------------------------------------------
 *  Copyright (c) 2008-2017 doLittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using doLittle.Commands;
using doLittle.Validation;
using FluentValidation;

namespace doLittle.Commands.Validation
{
    /// <summary>
    /// Defines a marker interface for input level validator for a <see cref="ICommand"/>
    /// </summary>
    /// <remarks>
    /// Types inheriting from this interface and also <see cref="ICanValidate{T}"/> will be automatically registered.
    /// You most likely want to subclass <see cref="CommandInputValidator{T}"/>.
    /// </remarks>
    public interface ICommandInputValidator : ICanValidate, IValidator
    {
    }
}