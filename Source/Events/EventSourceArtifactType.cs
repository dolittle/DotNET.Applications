/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using System;
using Dolittle.Artifacts;

namespace Dolittle.Events
{
    /// <summary>
    /// Represents a <see cref="IArtifactTypeMapFor{IEventSource}">application resource type</see> for 
    /// <see cref="IEventSource">event source</see>
    /// </summary>
    public class EventSourceArtifactType : IArtifactTypeMapFor<IEventSource>
    {
        /// <inheritdoc/>
        public string Identifier => "EventSource";
    }
}