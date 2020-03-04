// Copyright (c) Dolittle. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace Dolittle.Events.Processing
{
    /// <summary>
    /// Defines a system that knows about <see cref="RetryStrategy" /> retry strategies.
    /// </summary>
    public interface IRetryStrategies
    {
        /// <summary>
        /// Gets the <see cref="RetryStrategy" /> associated with this <see cref="ICanProcessEvent" /> event processor.
        /// </summary>
        /// <param name="processor">The <see cref="ICanProcessEvent" />.</param>
        RetryStrategy GetStrategyFor(ICanProcessEvent processor);
    }
}