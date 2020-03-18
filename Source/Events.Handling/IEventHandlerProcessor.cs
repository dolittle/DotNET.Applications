// Copyright (c) Dolittle. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace Dolittle.Events.Handling
{
    /// <summary>
    /// Defines a system that can handle processing for <see cref="EventHandler"/>.
    /// </summary>
    public interface IEventHandlerProcessor
    {
        /// <summary>
        /// Whether this <see cref="AbstractEventHandler" /> can be processed.
        /// </summary>
        /// <param name="eventHandler">The <see cref="AbstractEventHandler" />.</param>
        /// <returns>true if it can be processed, false if not.</returns>
        bool CanProcess(AbstractEventHandler eventHandler);

        /// <summary>
        /// Start processing for a specific <see cref="AbstractEventHandler"> event handler</see>.
        /// </summary>
        /// <param name="eventHandler"><see cref="AbstractEventHandler"/> to start processing.</param>
        void Start(AbstractEventHandler eventHandler);
    }
}