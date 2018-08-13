using System;
using System.Collections.Generic;
using System.Linq;
using Dolittle.Applications;
using Dolittle.Applications.Configuration;
using Dolittle.Collections;

namespace Dolittle.Artifacts.Tools
{
    internal static class BoundedContextConfigurationUtilities
    {
        internal enum BoundedContextRetrievalResult : int 
        {
            NewBoundedContextConfig,
            HasTopology
        } 

        /// <summary>
        /// Loads inn the <see cref="BoundedContextConfiguration"/> and validates the basic structure of the <see cref="BoundedContextConfiguration"/>
        /// </summary>
        /// <param name="manager">The <see cref="BoundedContextConfigurationManager"/> responsible for loading inn the <see cref="BoundedContextConfiguration"/></param>
        /// <param name="configuration">Outgoing <see cref="BoundedContextConfiguration"/></param>
        internal static BoundedContextRetrievalResult RetrieveConfiguration(IBoundedContextConfigurationManager manager, out BoundedContextConfiguration configuration)
        {
            configuration = manager.Load();
            ThrowBoundedContextConfigurationIsInvalid(configuration);

            if (IsNewConfiguration(configuration)) 
                return BoundedContextRetrievalResult.NewBoundedContextConfig;

            ThrowIfTopologyIsInvalid(configuration.UseModules, configuration.Topology);
            return BoundedContextRetrievalResult.HasTopology;
        }

        /// <summary>
        /// Retrieves a list of <see cref="FeatureDefinition"/> from the <see cref="BoundedContextConfiguration"/>
        /// </summary>
        /// <param name="configuration"></param>
        internal static IEnumerable<FeatureDefinition> RetrieveFeatures(BoundedContextConfiguration configuration)
        {
            if (configuration.UseModules) return configuration.Topology.Modules.SelectMany(_ => _.Features);     
            else return  configuration.Topology.Features;
            
        }

        internal static Dictionary<Feature, FeatureName> RetrieveAllFeatureIds(BoundedContextConfiguration configuration)
        {
            var featureMap = new Dictionary<Feature, FeatureName>();
            
            AddAllFeaturesToMap(RetrieveFeatures(configuration), ref featureMap);

            return featureMap;
        }

        static void AddAllFeaturesToMap(IEnumerable<FeatureDefinition> features, ref Dictionary<Feature, FeatureName> map)
        {
            foreach (var feature in features)
            {
                map.Add(feature.Feature, feature.Name);
                AddAllFeaturesToMap(feature.SubFeatures, ref map);
            }
        }

        internal static void ValidateTopology(BoundedContextConfiguration configuration)
        {
            ThrowIfDuplicateId(configuration);
        }

        static void ThrowIfDuplicateId(BoundedContextConfiguration configuration)
        {
            var idMap = new Dictionary<Guid, string>();
            bool hasDuplicateId = false;

            if (configuration.UseModules)
            {
                foreach (var module in configuration.Topology.Modules)
                {
                    if (idMap.ContainsKey(module.Module))
                    {
                        hasDuplicateId = true;
                        var name = idMap[module.Module];

                        ConsoleLogger.LogError(
                            $"Duplicate id found in bounded-context topology.\n" +
                            $"The id: {module.Module.Value} is already occupied by the Module/Feature: {name} ");
                    }
                    else
                    {
                        idMap.Add(module.Module, module.Name);
                    }
                    ThrowIfDuplicateId(module.Features, ref idMap, ref hasDuplicateId);
                }
            }
            else 
            {
                ThrowIfDuplicateId(configuration.Topology.Features, ref idMap, ref hasDuplicateId);
            }

            if (hasDuplicateId)
            {
                throw new InvalidTopology("Bounded context topology has one or more Features/Modules with the same ID");
            }
        }

        static void ThrowIfDuplicateId(IEnumerable<FeatureDefinition> features, ref Dictionary<Guid, string> idMap, ref bool hasDuplicateId)
        {
            foreach (var feature in features)
            {
                if (idMap.ContainsKey(feature.Feature))
                {

                    hasDuplicateId = true;
                    var name = idMap[feature.Feature];
                    
                    ConsoleLogger.LogError(
                        $"Duplicate id found in bounded-context topology.\n" +
                        $"The id: {feature.Feature.Value} is already occupied by the Module/Feature: {name} ");
                }
                else
                {
                    idMap.Add(feature.Feature, feature.Name);
                }
                ThrowIfDuplicateId(feature.SubFeatures, ref idMap, ref hasDuplicateId);
            }
        }
        static void ThrowBoundedContextConfigurationIsInvalid(BoundedContextConfiguration config)
        {
            if (config.Application == null || config.Application.Value.Equals(Guid.Empty)) 
                throw new InvalidBoundedContextConfiguration("Application is required and must cannot be an empty Guid");
            
            if (config.BoundedContext == null || config.BoundedContext.Value.Equals(Guid.Empty))
                throw new InvalidBoundedContextConfiguration("BoundedContext is required and must cannot be an empty Guid");
                
            
            if (config.BoundedContextName == null 
                || string.IsNullOrEmpty(config.BoundedContextName)
                || string.IsNullOrWhiteSpace(config.BoundedContextName)
                )
                throw new InvalidBoundedContextConfiguration("BoundedContextName is required and must cannot be an empty string or whitespace");
        }

        static void ThrowIfTopologyIsInvalid(bool useModules, TopologyConfiguration topology)
        {
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