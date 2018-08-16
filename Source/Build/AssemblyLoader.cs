/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.Loader;
using Microsoft.Extensions.DependencyModel;
using Microsoft.Extensions.DependencyModel.Resolution;

namespace Dolittle.Build
{
    /// <summary>
    /// Represents a system that is capable of loading assemblies out of current <see cref="AssemblyLoadContext"/>
    /// Based on : https://www.codeproject.com/Articles/1194332/Resolving-Assemblies-in-NET-Core
    /// </summary>
    public class AssemblyLoader : IDisposable
    {
        readonly ICompilationAssemblyResolver _assemblyResolver;

        /// <summary>
        /// Initializes a new instance of <see cref="AssemblyLoader"/>
        /// </summary>
        /// <param name="path">Path to the <see cref="Assembly"/> to load</param>
        public AssemblyLoader(string path)
        {
            Assembly = AssemblyLoadContext.Default.LoadFromAssemblyPath(path);
            this.DependencyContext = DependencyContext.Load(Assembly);

            _assemblyResolver = new CompositeCompilationAssemblyResolver(new ICompilationAssemblyResolver[]
            {
                new AppBaseCompilationAssemblyResolver(Path.GetDirectoryName(path)),
                    new ReferenceAssemblyPathResolver(),
                    new PackageCompilationAssemblyResolver()
            });

            this.AssemblyLoadContext = AssemblyLoadContext.GetLoadContext(Assembly);
            this.AssemblyLoadContext.Resolving += OnResolving;
        }

        /// <summary>
        /// Gets the loaded root assembly
        /// </summary>
        public Assembly Assembly { get; }

        /// <summary>
        /// Gets the <see cref="DependencyContext"/> for the <see cref="Assembly"/>
        /// </summary>
        public DependencyContext DependencyContext { get; }

        /// <summary>
        /// Gets the <see cref="AssemblyLoadContext"/> for the <see cref="Assembly"/>
        /// </summary>
        public AssemblyLoadContext AssemblyLoadContext { get;}


        /// <summary>
        /// Get assemblies that are referenced as project references to the loaded assembly
        /// </summary>
        /// <returns>Project <see cref="IEnumerable{Assembly}">assemblies</see></returns>
        public IEnumerable<Assembly>    GetProjectReferencedAssemblies()
        {
            var libraries = this.DependencyContext.RuntimeLibraries.Cast<RuntimeLibrary>()
                    .Where(_ => _.RuntimeAssemblyGroups.Count() > 0 && _.Type.ToLowerInvariant() == "project");
            return libraries
                    .Select(_ => Assembly.Load(_.Name))
                    .ToArray();
        }

        /// <inheritdoc/>
        public void Dispose()
        {
            this.AssemblyLoadContext.Resolving -= OnResolving;
        }

        Assembly OnResolving(AssemblyLoadContext context, AssemblyName name)
        {
            bool NamesMatch(RuntimeLibrary runtime)
            {
                return string.Equals(runtime.Name, name.Name, StringComparison.OrdinalIgnoreCase);
            }

            var library = this.DependencyContext.RuntimeLibraries.FirstOrDefault(NamesMatch);
            if (library != null)
            {
                var wrapper = new CompilationLibrary(
                    library.Type,
                    library.Name,
                    library.Version,
                    library.Hash,
                    library.RuntimeAssemblyGroups.SelectMany(g => g.AssetPaths),
                    library.Dependencies,
                    library.Serviceable);

                var assemblies = new List<string>();
                _assemblyResolver.TryResolveAssemblyPaths(wrapper, assemblies);
                if (assemblies.Count > 0)
                {
                    return this.AssemblyLoadContext.LoadFromAssemblyPath(assemblies[0]);
                }
            }

            return null;
        }
    }
}