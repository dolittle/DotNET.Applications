/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using System;

using Microsoft.Extensions.Logging;


namespace Dolittle.Build
{
    // Todo: 
    // - Consider having an option that can be passed in to say a base namespace. E.g. for Web projects (see Sentry) - we tend to have Features/ root folder.
    //   This configuration should then be optional and default set to empty. The MSBuild task should expose a variable that can be set in a <PropertyGroup/>
    //   The base namespace would be from the second segment - after tier segment
    //
    class Program
    {
        static int Main(string[] args)
        {
            if (args.Length != 1)
            {
                Console.WriteLine("Error consolidating artifacts; missing argument for name of assembly to consolidate");
                return 1;
            }
            try
            {
                while (!System.Diagnostics.Debugger.IsAttached)
                {
                    System.Threading.Thread.Sleep(10);
                }
                SetupConfigurationManagers();

                
                
                var types = GetArtifactsFromAssembly(assemblyLoader);

                var typePaths = ExtractTypePaths(types, boundedContextConfiguration.ExcludedNamespaceMap);
                
                
            }
            catch (Exception ex)
            {
                ConsoleLogger.LogError("Error consolidating artifacts;");
                ConsoleLogger.LogError(ex.Message);
                return 1;
            }

            return 0;
        }

        static void SetupConfigurationManagers()
        {
            var container = new ActivatorContainer();
            var converterProviders = new FixedInstancesOf<ICanProvideConverters>(new []
            {
                new Dolittle.Concepts.Serialization.Json.ConverterProvider()
            });

            var serializer = new Serializer(container, converterProviders);
            _boundedContextConfigurationManager = new BoundedContextConfigurationManager(serializer);
            _artifactsConfigurationManager = new ArtifactsConfigurationManager(serializer);
        }
        

        static Type[] GetArtifactsFromAssembly(AssemblyLoader assemblyLoader)
        {
            return assemblyLoader
                .GetProjectReferencedAssemblies()
                .SelectMany(_ => _.ExportedTypes)
                .Where(_ =>
                    _artifactTypes
                    .Any(at => at.Type.IsAssignableFrom(_)))
                .ToArray();
        }

        static string[] ExtractTypePaths(Type[] types, Dictionary<Area, IEnumerable<string>> excludeMap)
        {
            return types
                .Select(type => ExtractTypePath(type, excludeMap))
                .Where(_ => _.Length > 0)
                .Distinct()
                .ToArray();
        }
        static string ExtractTypePath(Type type, Dictionary<Area, IEnumerable<string>> excludeMap)
        {
            var area = new Area(){Value = type.Namespace.Split(NamespaceSeperator).First()};
            var segmentList = type.Namespace.Split(NamespaceSeperator).Skip(1).ToList();
            if (excludeMap.ContainsKey(area))
            {
                foreach (var segment in excludeMap[area]) 
                    segmentList.Remove(segment);
            }
            
            return string.Join(NamespaceSeperator, segmentList);
        }
        
        static void ThrowIfArtifactWithNoModuleOrFeature(Type[] types)
        {
            bool hasInvalidArtifact = false;
            foreach(var type in types)
            {
                var numSegments = type.Namespace.Split(NamespaceSeperator).Count();
                if (numSegments < 1) 
                {
                    hasInvalidArtifact = true;
                    ConsoleLogger.LogError($"Artifact {type.Name} with namespace = {type.Namespace} is invalid");
                }
            }
            if (hasInvalidArtifact) throw new InvalidArtifact();
        }

        static void ThrowIfContainsInvalidTypePath(string[] typePaths, bool useModules)
        {
            bool hasInvalidTypePath = false;
            foreach(var path in typePaths)
            {
                var numSegments = path.Split(NamespaceSeperator).Count();
                if (useModules && numSegments < 2) 
                {
                    hasInvalidTypePath = true;
                    ConsoleLogger.LogError($"Artifact with type path (a Module name + Feature names composition) {path} is invalid");
                }
                if (hasInvalidTypePath) throw new InvalidArtifact();
            }
        }

        static void AddExistingArtifactPaths(BoundedContextConfiguration boundedContextConfiguration, ref List<string> existingArtifactPaths)
        {
            if (boundedContextConfiguration.UseModules ) 
            {
               foreach (var module in boundedContextConfiguration.Topology.Modules)
                   existingArtifactPaths.AddRange(GetArtifactPathsFor(module.Features, module.Name));
            }
               
            else 
                existingArtifactPaths.AddRange(GetArtifactPathsFor(boundedContextConfiguration.Topology.Features));
            
        }
        
        static IList<string> GetArtifactPathsFor(IEnumerable<FeatureDefinition> features, string parent = "")
        {
            var paths = new List<string>();
            features.ForEach(_ =>
            {
                var featurePath = new List<string>();
                if( !string.IsNullOrEmpty(parent) ) featurePath.Add($"{parent}");
                featurePath.Add(_.Name);
                var featurePathAsString = string.Join(NamespaceSeperator, featurePath);
                paths.Add(featurePathAsString);
                paths.AddRange(GetArtifactPathsFor(_.SubFeatures, featurePathAsString));
            });

            return paths;
        }

        static int HandleArtifactOfType(Type artifactType, ArtifactsConfiguration artifactsConfiguration, IEnumerable<Type> types, BoundedContextConfiguration boundedContextConfiguration, string typeName, Expression<Func<ArtifactsByTypeDefinition, IEnumerable<ArtifactDefinition>> > targetPropertyExpression)
        {
            var targetProperty = targetPropertyExpression.GetPropertyInfo();

            var newArtifacts = 0;
            var artifacts = types.Where(_ => artifactType.IsAssignableFrom(_));
            
            artifacts.ForEach(artifact =>
            {
                var feature = boundedContextConfiguration.FindMatchingFeature(artifact.Namespace);
                if (feature != null)
                {
                    ArtifactsByTypeDefinition artifactsByTypeDefinition;

                    if (artifactsConfiguration.Artifacts.ContainsKey(feature.Feature))
                        artifactsByTypeDefinition = artifactsConfiguration.Artifacts[feature.Feature];
                    else
                    {
                        artifactsByTypeDefinition = new ArtifactsByTypeDefinition();
                        artifactsConfiguration.Artifacts[feature.Feature] = artifactsByTypeDefinition;
                    } 
                    var existingArtifacts = targetProperty.GetValue(artifactsByTypeDefinition) as IEnumerable<ArtifactDefinition>;
                    
                    if (!existingArtifacts.Any(_ => _.Type.GetActualType() == artifact))
                    {
                        var newAndExistingArtifacts = new List<ArtifactDefinition>(existingArtifacts);
                        var artifactDefinition = new ArtifactDefinition
                        {
                            Artifact = ArtifactId.New(),
                            Generation = ArtifactGeneration.First,
                            Type = ClrType.FromType(artifact)
                        };
                        Console.WriteLine($"Adding '{artifact.Name}' as a new {typeName} artifact with identifier '{artifactDefinition.Artifact}'");
                        newAndExistingArtifacts.Add(artifactDefinition);

                        newArtifacts++;

                        targetProperty.SetValue(artifactsByTypeDefinition, newAndExistingArtifacts);
                    }
                    
                }
            });
            return newArtifacts;
        }
    }
}