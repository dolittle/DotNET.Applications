// Copyright (c) Dolittle. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;

namespace Dolittle.Events.Processing
{
    /// <summary>
    /// Decorates a method to indicate the Event Handler Id of the Event Handler class.
    /// </summary>
    [AttributeUsage(AttributeTargets.Class)]
    public class RetryProcessingAttribute : Attribute
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="RetryProcessingAttribute"/> class.
        /// </summary>
        /// <param name="id">The <see cref="Guid" /> identifier.</param>
        public RetryProcessingAttribute(string id)
        {
        }
    }
    public interface IRetryStrategies
    {
        RetryStrategy GetStrategyFor(Type type);

        
    }
}