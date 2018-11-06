/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using System;
using System.Collections.Generic;
using System.Linq;
using Dolittle.Applications.Configuration;

using Dolittle.Collections;
using Dolittle.Serialization.Json;

namespace Dolittle.Build.Topology
{
    /// <summary>
    /// Represents a class that can build a valid <see cref="BoundedContextConfiguration"/>
    /// </summary>     
    public class TopologyBuilder
    {
        readonly Type[] _artifactTypes;
        readonly IBuildToolLogger _logger;

        BoundedContextTopology _configuration;

        /// <summary>
        /// Instantiates an instance of <see cref="TopologyBuilder"/>
        /// </summary>
        /// <param name="artifacts">The discovered types of artifacts in the Bounded Context's assemblies</param>
        /// <param name="boundedContextTopology">The <see cref="BoundedContextConfiguration"/> that will be modified, validated and returned from Build</param>
        /// <param name="logger"></param>
        public TopologyBuilder(Type[] artifacts, BoundedContextTopology boundedContextTopology, IBuildToolLogger logger)
        {
            _artifactTypes = artifacts;
            _logger = logger;
            _configuration = boundedContextTopology;
        }
        /// <summary>
        /// Builds a valid <see cref="BoundedContextConfiguration"/>
        /// </summary>
        /// <returns></returns>
        public Applications.Configuration.Topology Build()
        {
            ThrowIfLoadedConfigurationIsInvalid();  
            var isNewConfiguration = IsNewConfiguration();

            var typePaths = ExtractTypePaths(_artifactTypes);
            ThrowIfContainsInvalidTypePath(typePaths);

            var existingArtifactPaths = GetExistingArtifactPathsFromTopology();

            var missingPaths = isNewConfiguration? 
                    typePaths
                    : typePaths.Where(_ => !existingArtifactPaths.Any(ap => ap == _)).ToArray();

            if (missingPaths.Any()) 
            {
                Program.NewTopology = true;
                AddPathsToBoundedContextConfiguration(missingPaths);
            }

            _configuration.Topology.ValidateTopology(_configuration.UseModules, _logger);

            return _configuration.Topology;
        }

        string[] ExtractTypePaths(Type[] types)
        {
            return types
                .Select(type => ExtractTypePath(type))
                .Where(_ => _.Length > 0)
                .Distinct()
                .ToArray();
        }

        string ExtractTypePath(Type type)
        {
            var area = new Area(){Value = type.Namespace.Split(".").First()};
            var segmentList = type.Namespace.Split(".").Skip(1).ToList();
            
            if (_configuration.NamespaceSegmentsToStrip.ContainsKey(area))
            {
                foreach (var segment in _configuration.NamespaceSegmentsToStrip[area]) 
                    segmentList.Remove(segment);
            }
            
            return string.Join(".", segmentList);
        }

        IEnumerable<string> GetExistingArtifactPathsFromTopology()
        {
            var existingArtifactPaths = new List<string>();
            if (_configuration.UseModules ) 
            {
               foreach (var module in _configuration.Topology.Modules)
                   existingArtifactPaths.AddRange(GetArtifactPathsFor(module.Features, module.Name));
            }
            else 
                existingArtifactPaths.AddRange(GetArtifactPathsFor(_configuration.Topology.Features));

            return existingArtifactPaths;
            
        }

        void AddPathsToBoundedContextConfiguration(string[] typePaths)
        {
            if (_configuration.UseModules)
                AddModulesAndFeatures(typePaths);
            else
                AddFeatures(typePaths);
        }

        void AddModulesAndFeatures(string[] missingPaths)
        {
            var modules = new List<ModuleDefinition>(_configuration.Topology.Modules);

            foreach(var path in missingPaths)
                modules.Add(path.GetModuleFromPath());

            _configuration.Topology.Modules = modules.GetCollapsedModules();
        }

        void AddFeatures(string[] missingPaths)
        {
            var features = new List<FeatureDefinition>(_configuration.Topology.Features);
            foreach (var path in missingPaths)
                features.Add(path.GetFeatureFromPath());

            _configuration.Topology.Features = features.GetCollapsedFeatures();
        }
        static IEnumerable<string> GetArtifactPathsFor(IEnumerable<FeatureDefinition> features, string parent = "")
        {
            var paths = new List<string>();
            features.ForEach(_ =>
            {
                var featurePath = new List<string>();
                if( !string.IsNullOrEmpty(parent) ) featurePath.Add($"{parent}");
                featurePath.Add(_.Name);

                var featurePathAsString = string.Join(".", featurePath);
                paths.Add(featurePathAsString);
                paths.AddRange(GetArtifactPathsFor(_.SubFeatures, featurePathAsString));
            });

            return paths;
        }

        void ThrowIfLoadedConfigurationIsInvalid()
        {       
            if (_configuration.NamespaceSegmentsToStrip.Any())
                ThrowIfNamespaceSegmentsToStripHasEmptyValue();

            if (!IsNewConfiguration())
                ThrowIfLoadedTopologyIsInvalid(_configuration.UseModules, _configuration.Topology);
        }

        void ThrowIfLoadedTopologyIsInvalid(bool useModules, Applications.Configuration.Topology topology)
        {
            if (useModules)
            {
                if (HasFeatures(topology))
                    throw new InvalidTopology("Topology cannot have root level Features when UseModules is true");
                
                if (topology.Modules == null)
                    throw new InvalidTopology("Topology must define a Modules list when UseModules is true");
            }
            else 
            {
                if (topology.Features == null)
                    throw new InvalidTopology("Topology must define a Feature list when UseModules is false");

                if (HasModules(topology))
                    throw new InvalidTopology("Topology cannot have Modules when UseModules is false");
            }
        }
        void ThrowIfNamespaceSegmentsToStripHasEmptyValue()
        {
            foreach (var entry in _configuration.NamespaceSegmentsToStrip) 
            {
                if (!entry.Value.Any() || entry.Value.Any(@namespace => string.IsNullOrEmpty(@namespace)))
                    throw new InvalidBoundedContextConfiguration($"A mapping of an excluded namespace cannot contain an empty namespace value.  Area {entry.Key.Value} has empty values.");
                
            }
        }

        void ThrowIfContainsInvalidTypePath(string[] typePaths)
        {
            var invalidPaths = new List<string>();
            foreach(var path in typePaths)
            {
                var numSegments = path.Split(".").Count();
                if (_configuration.UseModules && numSegments < 2) 
                {
                    invalidPaths.Add(path);
                    _logger.Error($"Artifact with type path (a Module name + Feature names composition) {path} is invalid");
                }
                if (invalidPaths.Any()) throw new InvalidArtifact();
            }
        }

        bool IsNewConfiguration() => 
            _configuration.Topology == null 
            || (! HasModules(_configuration.Topology) && ! HasFeatures(_configuration.Topology));

        static bool HasFeatures(Applications.Configuration.Topology topology) => 
            topology.Features != null && topology.Features.Any();

        static bool HasModules(Applications.Configuration.Topology topology) => 
            topology.Modules != null && topology.Modules.Any();
    }
}