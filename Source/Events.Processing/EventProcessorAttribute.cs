/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 * --------------------------------------------------------------------------------------------*/

using System;

namespace Dolittle.Events.Processing
{
    /// <summary>
    /// Decorates a method to indicate that the method is an Event Processor
    /// </summary>
    [AttributeUsage(AttributeTargets.Method)]
    public class EventProcessorAttribute : Attribute
    {
        public Guid Id { get; }

        /// <summary>
        /// Instantiates an instance of the <see cref="EventProcessorAttribute" />
        /// </summary>
        /// <param name="id">The <see cref="Guid" /> identifier</param>
        public EventProcessorAttribute(string id)
        {
            Id = Guid.Parse(id);
        }
    }
}