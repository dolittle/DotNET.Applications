// Copyright (c) Dolittle. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace Dolittle.Events.Handling
{
    /// <summary>
    /// Defines a system that knows about <see cref="IEventHandlerProcessor" />.
    /// </summary>
    public interface IEventHandlerProcessors
    {
        /// <summary>
        /// Start processing for a specific <see cref="AbstractEventHandler"> event handler</see>.
        /// </summary>
        /// <param name="eventHandler"><see cref="AbstractEventHandler"/> to start processing.</param>
        void Start(AbstractEventHandler eventHandler);
    }
}