using System.Collections.Generic;
using System.Linq;
using Dolittle.Applications;
using Dolittle.Applications.Configuration;

namespace Dolittle.Artifacts.Tools
{
    internal static class TopologyExtensions
    {
        internal static IList<ModuleDefinition> GetCollapsedModules(this TopologyConfiguration topology)
        {
            var modules = new List<ModuleDefinition>();

            foreach (var group in topology.Modules.GroupBy(_ => _.Name))
            {
                var module = group.ElementAt(0);
                var features = new List<FeatureDefinition>(module.Features);
                features.AddRange(group.Skip(1).SelectMany(_ => _.Features));
                module.Features = CollapseFeatures(features.GroupBy(_ => _.Name));

                modules.Add(module);
            }

            return modules;
        }
        internal static IList<FeatureDefinition> GetCollapsedFeatures(this TopologyConfiguration topology)
        {
            var features = new List<FeatureDefinition>();

            foreach (var group in topology.Features.GroupBy(_ => _.Name))
            {
                var feature = group.ElementAt(0);
                var subFeatures = new List<FeatureDefinition>(feature.SubFeatures);
                subFeatures.AddRange(group.Skip(1).SelectMany(_ => _.SubFeatures));
                feature.SubFeatures = CollapseFeatures(subFeatures.GroupBy(_ => _.Name));

                features.Add(feature);
            }
            return features;
        }
        static IList<FeatureDefinition> CollapseFeatures(IEnumerable<IGrouping<FeatureName, FeatureDefinition>> featureGroups)
        {
            var features = new List<FeatureDefinition>();

            foreach (var group in featureGroups)
            {
                var feature = group.ElementAt(0);
                var subFeatures = new List<FeatureDefinition>(feature.SubFeatures);
                subFeatures.AddRange(group.Skip(1).SelectMany(_ => _.SubFeatures));
                feature.SubFeatures = CollapseFeatures(subFeatures.GroupBy(_ => _.Name));

                features.Add(feature);
            }
            return features;
        }
    }
}