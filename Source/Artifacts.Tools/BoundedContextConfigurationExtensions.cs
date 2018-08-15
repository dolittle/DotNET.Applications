/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using System;
using System.Collections.Generic;
using System.Linq;
using Dolittle.Applications;
using Dolittle.Applications.Configuration;

namespace Dolittle.Artifacts.Tools
{
    internal static class BoundedContextConfigurationExtensions
    {
        const string NamespaceSeperator = Program.NamespaceSeperator;

         /// <summary>
        /// Retrieves a list of <see cref="FeatureDefinition"/> from the <see cref="BoundedContextConfiguration"/>
        /// </summary>
        /// <param name="configuration"></param>
        internal static IEnumerable<FeatureDefinition> RetrieveFeatures(this BoundedContextConfiguration configuration)
        {
            if (configuration.UseModules) return configuration.Topology.Modules.SelectMany(_ => _.Features);     
            else return  configuration.Topology.Features;
            
        }

        internal static Dictionary<Feature, FeatureName> RetrieveAllFeatureIds(this BoundedContextConfiguration configuration)
        {
            var featureMap = new Dictionary<Feature, FeatureName>();
            
            AddAllFeaturesToMap(RetrieveFeatures(configuration), ref featureMap);

            return featureMap;
        }

        internal static FeatureDefinition FindMatchingFeature(this BoundedContextConfiguration boundedContextConfiguration, string @namespace)
        {
            var area = new Area(){Value = @namespace.Split(NamespaceSeperator).First()};
            var segments = @namespace.Split(NamespaceSeperator).Skip(1).ToArray();
            
            if (boundedContextConfiguration.ExcludedNamespaceMap.ContainsKey(area))
            {
                var newSegments = new List<string>(segments);
                foreach (var segment in boundedContextConfiguration.ExcludedNamespaceMap[area]) 
                    newSegments.Remove(segment);
                segments = newSegments.ToArray();
            }

            if (boundedContextConfiguration.UseModules)
            {
                var matchingModule = boundedContextConfiguration.Topology.Modules
                    .SingleOrDefault(module => module.Name.Value.Equals(segments[0]));
                
                //TODO: matchingModule == null => Module not found, error
                if (segments.Count() < 2) throw new Exception("This should not happen");//TODO: Better exception
                return FindMatchingFeature(segments.Skip(1).ToArray(), matchingModule.Features);
            }
            
            return FindMatchingFeature(segments,boundedContextConfiguration.Topology.Features);
        }
        internal static void ValidateTopology(this BoundedContextConfiguration configuration)
        {
            ThrowIfDuplicateId(configuration);
        }

         static void AddAllFeaturesToMap(IEnumerable<FeatureDefinition> features, ref Dictionary<Feature, FeatureName> map)
        {
            foreach (var feature in features)
            {
                map.Add(feature.Feature, feature.Name);
                AddAllFeaturesToMap(feature.SubFeatures, ref map);
            }
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
        static FeatureDefinition FindMatchingFeature(string[] segments, IEnumerable<FeatureDefinition> features)
        {
            var featureName = segments.Count() > 0? segments[0] : "";
            if (string.IsNullOrEmpty(featureName)) 
                throw new Exception("Missing feature");//TODO: throw new MissingFeature
            
            var matchingFeature = features.SingleOrDefault(feature => feature.Name.Value.Equals(segments[0]));

            if (matchingFeature == null) 
                throw new Exception("Missing feature");//TODO: throw new MissingFeature

            if (segments.Count() == 1) return matchingFeature;

            return FindMatchingFeature(segments.Skip(1).ToArray(), matchingFeature.SubFeatures);
        }
    }
}