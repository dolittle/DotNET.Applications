/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using System.Collections;
using System.Collections.Generic;
using Dolittle.Commands;
using Dolittle.Events;
using Dolittle.Events.Processing;
using Dolittle.Execution;
using Dolittle.Queries;
using Dolittle.ReadModels;

namespace Dolittle.Build
{
    /// <summary>
    /// Represents a class that's basically a collection of Dolittle's native artifact types
    /// </summary>
    public class ArtifactTypes : IEnumerable<ArtifactType>
    {
        List<ArtifactType> _artifactTypes = new List<ArtifactType>
        {
            new ArtifactType { Type = typeof(ICommand), TypeName = "command", TargetPropertyExpression = a => a.Commands },
            new ArtifactType { Type = typeof(IEvent), TypeName = "event", TargetPropertyExpression = a => a.Events },
            new ArtifactType { Type = typeof(IEventSource), TypeName = "event source", TargetPropertyExpression = a => a.EventSources },
            new ArtifactType { Type = typeof(IReadModel), TypeName = "read model", TargetPropertyExpression = a => a.ReadModels },
            new ArtifactType { Type = typeof(IQuery), TypeName = "query", TargetPropertyExpression = a => a.Queries }
        };

        /// <inheritdoc/>
        public IEnumerator<ArtifactType> GetEnumerator()
        {
            return _artifactTypes.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return _artifactTypes.GetEnumerator();
        }
    }
}