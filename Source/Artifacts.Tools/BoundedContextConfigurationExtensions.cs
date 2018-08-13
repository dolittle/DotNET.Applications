using System;
using System.Collections.Generic;
using System.Linq;
using Dolittle.Applications.Configuration;

namespace Dolittle.Artifacts.Tools
{
    internal static class BoundedContextConfigurationExtensions
    {
        const string NamespaceSeperator = Program.NamespaceSeperator;
        internal static FeatureDefinition FindMatchingFeature(this BoundedContextConfiguration boundedContextConfiguration, string @namespace)
        {
            var segments = @namespace.Split(NamespaceSeperator).Skip(1).ToArray();
            
            if (boundedContextConfiguration.UseModules)
            {
                var matchingModule = boundedContextConfiguration.Topology.Modules
                    .SingleOrDefault(module => module.Name.Value.Equals(segments[0]));
                
                //TODO: matchingModule == null => Module not found, error
                if (segments.Count() < 2) throw new Exception("This should not happen");//TODO: Better exception
                return FindMatchingFeature(segments.Skip(1).ToArray(), matchingModule.Features);
            }
            
            return FindMatchingFeature(segments, boundedContextConfiguration.Topology.Features);
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