// Copyright (c) Dolittle. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;

namespace Dolittle.Events.Processing
{
    /// <summary>
    /// Decorates a system that processes an event with a strategy retrying failed events.
    /// </summary>
    [AttributeUsage(AttributeTargets.Class)]
    public class RetryProcessingAttribute : Attribute
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="RetryProcessingAttribute"/> class.
        /// </summary>
        public RetryProcessingAttribute()
        {
        }
    }
}