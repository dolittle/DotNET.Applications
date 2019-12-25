// Copyright (c) Dolittle. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace Dolittle.Commands.Validation
{
    /// <summary>
    /// Represent a null or non-existant validator.
    /// </summary>
    /// <remarks>
    /// Always returns an empty validation result collection.
    /// </remarks>
    /// <typeparam name="T">Type of <see cref="ICommand"/>.</typeparam>
    public class NullCommandInputValidatorFor<T> : CommandInputValidatorFor<T>
        where T : class, ICommand
    {
    }
}