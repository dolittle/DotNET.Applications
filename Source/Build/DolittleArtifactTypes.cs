using Dolittle.Execution;
using Dolittle.Events;
using Dolittle.Events.Processing;
using Dolittle.Queries;
using Dolittle.ReadModels;
using Dolittle.Commands;

namespace Dolittle.Build
{
    /// <summary>
    /// Represents a class that's basically a collection of Dolittle's native artifact types
    /// </summary>
    public class DolittleArtifactTypes
    {
        /// <summary>
        /// The list of <see cref="ArtifactType"/> that are native to Dolittle
        /// </summary>
        public ArtifactType[] ArtifactTypes = new ArtifactType[]
            {
                new ArtifactType { Type = typeof(ICommand), TypeName = "command", TargetPropertyExpression = a => a.Commands },
                new ArtifactType { Type = typeof(IEvent), TypeName = "event", TargetPropertyExpression = a => a.Events },
                new ArtifactType { Type = typeof(ICanProcessEvents), TypeName = "event processor", TargetPropertyExpression = a => a.EventProcessors },
                new ArtifactType { Type = typeof(IEventSource), TypeName = "event source", TargetPropertyExpression = a => a.EventSources },
                new ArtifactType { Type = typeof(IReadModel), TypeName = "read model", TargetPropertyExpression = a => a.ReadModels },
                new ArtifactType { Type = typeof(IQuery), TypeName = "query", TargetPropertyExpression = a => a.Queries }
            };

    }
}