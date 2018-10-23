/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/

using Dolittle.Events;
using Dolittle.Runtime.Events;

namespace Specs.Feature
{
    public abstract class AbstractEventSource : IEventSource
    {
        public EventSourceId EventSourceId => throw new System.NotImplementedException();

        public EventSourceVersion Version => throw new System.NotImplementedException();

        public UncommittedEvents UncommittedEvents => throw new System.NotImplementedException();

        public void Apply(IEvent @event)
        {
            throw new System.NotImplementedException();
        }

        public void Commit()
        {
            throw new System.NotImplementedException();
        }

        public void Dispose()
        {
            throw new System.NotImplementedException();
        }

        public void FastForward(EventSourceVersion version)
        {
            throw new System.NotImplementedException();
        }

        public void ReApply(CommittedEvents eventStream)
        {
            throw new System.NotImplementedException();
        }

        public void Rollback()
        {
            throw new System.NotImplementedException();
        }
    }
}