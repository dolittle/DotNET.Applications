// Copyright (c) Dolittle. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;

namespace Dolittle.Validation
{
    /// <summary>
    /// Exception that is thrown if a validator type is of wrong type.
    /// </summary>
    public class InvalidValidatorTypeException : ArgumentException
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="InvalidValidatorTypeException"/> class.
        /// </summary>
        /// <param name="message">The message.</param>
        public InvalidValidatorTypeException(string message)
            : base(message)
        {
        }
    }
}
