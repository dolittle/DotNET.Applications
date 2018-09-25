/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using System;
using System.Runtime.Serialization;

namespace Dolittle.Build
{
    /// <summary>
    /// The exception that gets thrown when a critical validation error is found on an event
    /// </summary>
    public class InvalidEvent : Exception
    {
        /// <summary>
        /// Instantiates an instance of <see cref="InvalidEvent"/>
        /// </summary>
        /// <param name="message"></param>
        public InvalidEvent(string message) : base(message)
        {
        }
    }
}