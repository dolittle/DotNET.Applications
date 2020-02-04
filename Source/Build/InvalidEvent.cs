// Copyright (c) Dolittle. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;

namespace Dolittle.Build
{
    /// <summary>
    /// Exception that gets thrown when a critical validation error is found on an event.
    /// </summary>
    public class InvalidEvent : Exception
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="InvalidEvent"/> class.
        /// </summary>
        public InvalidEvent()
            : base("There are critical errors on events")
        {
        }
    }
}