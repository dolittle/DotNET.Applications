/*---------------------------------------------------------------------------------------------
 *  Copyright (c) 2008-2017 doLittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using doLittle.Commands;

namespace doLittle.FluentValidation.Commands
{
    /// <summary>
    /// Represent a null or non-existant validator.
    /// </summary>
    /// <remarks>
    /// Always returns an empty validation result collection.
    /// </remarks>
    public class NullCommandBusinessValidator<T> : CommandBusinessValidator<T> where T : class, ICommand
    {
    }
}