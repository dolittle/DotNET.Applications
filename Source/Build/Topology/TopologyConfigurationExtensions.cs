/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using System;
using System.Collections.Generic;
using System.Linq;
using Dolittle.Applications.Configuration;
using Dolittle.Logging;

namespace Dolittle.Build.Topology
{
    /// <summary>
    /// Extensions for the topology of a <see cref="BoundedContextConfiguration"/>
    /// </summary>
    public static class TopologyConfigurationExtensions
    {
        /// <summary>
        /// Returns a collapsed list of <see cref="ModuleDefinition"/> 
        /// </summary>
        public static IReadOnlyList<ModuleDefinition> GetCollapsedModules(this IEnumerable<ModuleDefinition> modules)
        {
            var collapsedModules = new List<ModuleDefinition>();

            foreach (var group in modules.GroupBy(_ => _.Name))
            {
                var module = group.ElementAt(0);
                var features = new List<FeatureDefinition>(module.Features);
                features.AddRange(group.Skip(1).SelectMany(_ => _.Features));
                module.Features = features.GetCollapsedFeatures();

                collapsedModules.Add(module);
            }

            return collapsedModules;
        }
        /// <summary>
        /// Returns a collapsed list of <see cref="FeatureDefinition"/> 
        /// </summary>
        public static IReadOnlyList<FeatureDefinition> GetCollapsedFeatures(this IEnumerable<FeatureDefinition> features)
        {
            var collapsedFeatures = new List<FeatureDefinition>();

            foreach (var group in features.GroupBy(_ => _.Name))
            {
                var feature = group.ElementAt(0);
                var subFeatures = new List<FeatureDefinition>(feature.SubFeatures);
                subFeatures.AddRange(group.Skip(1).SelectMany(_ => _.SubFeatures));
                feature.SubFeatures = subFeatures.GetCollapsedFeatures();

                collapsedFeatures.Add(feature);
            }
            return collapsedFeatures;
        }
        /// <summary>
        /// Validates the <see cref="TopologyConfiguration"/>
        /// </summary>
        public static void ValidateTopology(this TopologyConfiguration topology, bool useModules, ILogger logger)
        {
            ThrowIfDuplicateId(topology, useModules, logger);
        }

        static void ThrowIfDuplicateId(TopologyConfiguration topology, bool useModules, ILogger logger)
        {
            var idMap = new Dictionary<Guid, string>();
            bool hasDuplicateId = false;

            if (useModules)
            {
                foreach (var module in topology.Modules)
                {
                    if (idMap.ContainsKey(module.Module))
                    {
                        hasDuplicateId = true;
                        var name = idMap[module.Module];

                        logger.Error(
                            $"Duplicate id found in bounded-context topology.\n" +
                            $"The id: {module.Module.Value} is already occupied by the Module/Feature: {name} ");
                    }
                    else
                    {
                        idMap.Add(module.Module, module.Name);
                    }
                    ThrowIfDuplicateId(module.Features, ref idMap, ref hasDuplicateId, logger);
                }
            }
            else 
            {
                ThrowIfDuplicateId(topology.Features, ref idMap, ref hasDuplicateId, logger);
            }

            if (hasDuplicateId)
            {
                throw new InvalidTopology("Bounded context topology has one or more Features/Modules with the same ID");
            }
        }
        static void ThrowIfDuplicateId(IEnumerable<FeatureDefinition> features, ref Dictionary<Guid, string> idMap, ref bool hasDuplicateId, ILogger logger)
        {
            foreach (var feature in features)
            {
                if (idMap.ContainsKey(feature.Feature))
                {
                    hasDuplicateId = true;
                    var name = idMap[feature.Feature];
                    
                    logger.Error(
                        $"Duplicate id found in bounded-context topology.\n" +
                        $"The id: {feature.Feature.Value} is already occupied by the Module/Feature: {name} ");
                }
                else
                {
                    idMap.Add(feature.Feature, feature.Name);
                }
                ThrowIfDuplicateId(feature.SubFeatures, ref idMap, ref hasDuplicateId, logger);
            }
        }
    }
}