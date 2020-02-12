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
        /// Start processing for a specific <see cref="EventHandler"/>.
        /// </summary>
        /// <param name="eventHandler"><see cref="EventHandler"/> to start processing.</param>
        void Start(EventHandler eventHandler);
    }
}