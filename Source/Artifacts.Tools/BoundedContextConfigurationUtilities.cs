using System.Linq;
using Dolittle.Applications.Configuration;

namespace Dolittle.Artifacts.Tools
{
    internal static class BoundedContextConfigurationUtilities
    {
        internal enum BoundedContextRetrievalResult : int 
        {
            NewBoundedContextConfig,
            HasTopology
        } 

        internal static BoundedContextRetrievalResult RetrieveConfiguration(IBoundedContextConfigurationManager manager, out BoundedContextConfiguration configuration)
        {
            configuration = manager.Load();
            ThrowBoundedContextConfigurationIsInvalid(configuration);

            if (IsNewConfiguration(configuration)) 
                return BoundedContextRetrievalResult.NewBoundedContextConfig;

            ThrowIfTopologyIsInvalid(configuration.UseModules, configuration.Topology);
            return BoundedContextRetrievalResult.HasTopology;
        }

        static void ThrowBoundedContextConfigurationIsInvalid(BoundedContextConfiguration config)
        {
            if (config.Application == null || config.Application.Value.Equals(System.Guid.Empty)) 
                throw new InvalidBoundedContextConfiguration("Application is required and must cannot be an empty Guid");
            
            if (config.BoundedContext == null || config.BoundedContext.Value.Equals(System.Guid.Empty))
                throw new InvalidBoundedContextConfiguration("BoundedContext is required and must cannot be an empty Guid");
                
            
            if (config.BoundedContextName == null 
                || string.IsNullOrEmpty(config.BoundedContextName)
                || string.IsNullOrWhiteSpace(config.BoundedContextName)
                )
                throw new InvalidBoundedContextConfiguration("BoundedContextName is required and must cannot be an empty string or whitespace");
        }

        static void ThrowIfTopologyIsInvalid(bool useModules, TopologyConfiguration topology)
        {
            // QUESTION: Throw if empty topology?
            if (useModules)
            {
                if (HasFeatures(topology))
                    throw new InvalidTopology("Topology cannot have root level Features when UseModules is true");
                
                if (topology.Modules == null) // Shouldn't happen if Modules is an IEnumerable
                    throw new InvalidTopology("Topology must define a Modules list when UseModules is true");
            }
            else 
            {
                if (topology.Features == null) // Shouldn't happen if Features is an IEnumerable
                    throw new InvalidTopology("Topology must define a Feature list when UseModules is false");

                if (HasModules(topology))
                    throw new InvalidTopology("Topology cannot have Modules when UseModules is false");
            }
        }

        static bool HasFeatures(TopologyConfiguration topology) => 
            topology.Features != null && topology.Features.Any();

        static bool HasModules(TopologyConfiguration topology) => 
            topology.Modules != null && topology.Modules.Any();

        static bool IsNewConfiguration(BoundedContextConfiguration config) => 
            config.Topology == null 
            || (! HasModules(config.Topology) && ! HasFeatures(config.Topology));
        
    }
}