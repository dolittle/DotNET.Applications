/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using System.Collections.Generic;
using System.Linq;
using Dolittle.Applications;
using Dolittle.Applications.Configuration;

namespace Dolittle.Artifacts.Tools
{
    internal static class TopologyExtensions
    {
        internal static IList<ModuleDefinition> GetCollapsedModules(this IEnumerable<ModuleDefinition> modules)
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
        internal static IList<FeatureDefinition> GetCollapsedFeatures(this IEnumerable<FeatureDefinition> features)
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
    }
}