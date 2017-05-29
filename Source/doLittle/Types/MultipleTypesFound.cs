/*---------------------------------------------------------------------------------------------
 *  Copyright (c) 2008-2017 doLittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using System;

namespace doLittle.Types
{
    /// <summary>
    /// The exception that is thrown when multiple types are found and not allowed
    /// </summary>
    public class MultipleTypesFound : ArgumentException
    {
        /// <summary>
        /// Initializes an instance of <see cref="MultipleTypesFound"/>
        /// </summary>
        public MultipleTypesFound() {}

        /// <summary>
        /// Initializes an instance of <see cref="MultipleTypesFound"/>
        /// </summary>
        /// <param name="message">Message with details about the exception</param>
        public MultipleTypesFound(string message) : base(message) {}
    }
}