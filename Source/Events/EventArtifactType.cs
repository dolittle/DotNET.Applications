/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using System;
using Dolittle.Artifacts;

namespace Dolittle.Events
{
    /// <summary>
    /// Represents a <see cref="IArtifactTypeMapFor{IEvent}">application resource type</see> for 
    /// <see cref="IEvent">event</see>
    /// </summary>
    public class EventProcessor : IArtifactTypeMapFor<IEvent>
    {
        /// <inheritdoc/>
        public string Identifier => "Event";
    }
}