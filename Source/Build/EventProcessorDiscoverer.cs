using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Dolittle.Collections;
using Dolittle.Events.Processing;
using Dolittle.Lifecycle;
using Dolittle.Logging;

namespace Dolittle.Build
{
    
    /// <summary>
    /// Represents a class that can discover event processors
    /// </summary>
    public class EventProcessorDiscoverer
    {
        readonly static Type EventProcessorCollectionType = typeof(ICanProcessEvents);
        
        readonly AssemblyLoader _assemblyLoader;
        readonly ILogger _logger;

        MethodInfo[] _eventProcessors;

        /// <summary>
        /// Gets all the discovered event processors
        /// </summary>
        public IEnumerable<MethodInfo> GetAllEventProcessors() => 
            _eventProcessors
            ?? (_eventProcessors = DiscoverEventProcessors());
        /// <summary>
        /// Gets all the discovered event processors of a type
        /// </summary>
        /// <param name="type"></param>
        public IEnumerable<MethodInfo> GetEventProcessors(Type type) => 
            _eventProcessors.Where(_ => _.DeclaringType.Equals(type)) 
            ?? (_eventProcessors = DiscoverEventProcessors()).Where(_ => _.DeclaringType.Equals(type));
        
        /// <summary>
        /// Instantiates and instance of <see cref="EventProcessorDiscoverer"/>
        /// </summary>
        /// <param name="assemblyLoader"></param>
        /// <param name="logger"></param>
        public EventProcessorDiscoverer(AssemblyLoader assemblyLoader, ILogger logger)
        {
            _assemblyLoader = assemblyLoader;
            _logger = logger;
        }
        MethodInfo[] DiscoverEventProcessors()
        {
            _logger.Information("Discovering Event Processors");
            
            var startTime = DateTime.UtcNow;
            var types = GetTypesHoldingEventProcessorsFromAssembly();

            IList<MethodInfo> eventProcessors = new List<MethodInfo>();

            foreach (var type in types)
            {
                var found = false;
                type.GetMethods().ForEach(_ => 
                {
                    var eventProcessorId = _.EventProcessorId();
                    if (eventProcessorId.Value != null && ! eventProcessorId.Value.Equals(Guid.Empty)) 
                    {
                        found = true;
                        eventProcessors.Add(_);
                    }
                });
                if (! found)
                    _logger.Warning($"No event processor methods found in Event Processor collection class '{type.FullName}'. All methods that'll process events has to be marked with {typeof(EventProcessorAttribute).FullName} giving it a unique Event Processor Id.");
            }
            
            var endTime = DateTime.UtcNow;
            var deltaTime = endTime.Subtract(startTime);
            _logger.Information($"Finished event processor discovery process. (Took {deltaTime.TotalSeconds} seconds)");
            
            return eventProcessors.ToArray();
        }
        IEnumerable<Type> GetTypesHoldingEventProcessorsFromAssembly()
        {
            return _assemblyLoader
                .GetProjectReferencedAssemblies()
                .SelectMany(_ => _.ExportedTypes)
                .Where(_ =>
                    EventProcessorCollectionType.IsAssignableFrom(_));
        }
    }
}