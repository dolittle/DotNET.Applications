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
    /// <inheritdoc/>
    public class AssemblyLoader : IAssemblyLoader
    {
        readonly ICompilationAssemblyResolver _assemblyResolver;

        /// <summary>
        /// Initializes a new instance of <see cref="AssemblyLoader"/>
        /// </summary>
        /// <param name="assembly">Path to the <see cref="Assembly"/> to load</param>
        public AssemblyLoader(Assembly assembly)
        {
            Assembly = assembly;
            AssemblyLoadContext = AssemblyLoadContext.GetLoadContext(Assembly);
            AssemblyLoadContext.Resolving += OnResolving;

            DependencyContext = DependencyContext.Load(Assembly);

            var basePath = Path.GetDirectoryName(assembly.Location);

            _assemblyResolver = new CompositeCompilationAssemblyResolver(new ICompilationAssemblyResolver[]
            {
                new AppBaseCompilationAssemblyResolver(basePath),
                new ReferenceAssemblyPathResolver(),
                new PackageCompilationAssemblyResolver(),
                new PackageRuntimeStoreAssemblyResolver()
            });
        }

        /// <inheritdoc/>
        public Assembly Assembly { get; }

        /// <inheritdoc/>
        public DependencyContext DependencyContext {  get; }

        /// <inheritdoc/>
        public AssemblyLoadContext AssemblyLoadContext {  get; }

        /// <inheritdoc/>
        public IEnumerable<Assembly> GetProjectReferencedAssemblies()
        {
            var libraries = DependencyContext.RuntimeLibraries.Cast<RuntimeLibrary>()
                .Where(_ => _.RuntimeAssemblyGroups.Count() > 0 && _.Type.ToLowerInvariant() == "project");
            return libraries
                .Select(_ => Assembly.Load(_.Name))
                .ToArray();
        }

        /// <inheritdoc/>
        public void Dispose()
        {
            AssemblyLoadContext.Resolving -= OnResolving;
        }

        Assembly OnResolving(AssemblyLoadContext context, AssemblyName name)
        {
            bool NamesMatch(Library runtime)
            {
                return string.Equals(runtime.Name, name.Name, StringComparison.OrdinalIgnoreCase);
            }

            var library = DependencyContext.RuntimeLibraries.FirstOrDefault(NamesMatch);
            if (library != null)
            {
                var compileLibrary = new CompilationLibrary(
                    library.Type,
                    library.Name,
                    library.Version,
                    library.Hash,
                    library.RuntimeAssemblyGroups.SelectMany(g => g.AssetPaths),
                    library.Dependencies,
                    library.Serviceable,
                    library.Path,
                    library.HashPath);

                var assemblies = new List<string>();
                _assemblyResolver.TryResolveAssemblyPaths(compileLibrary, assemblies);
                if (assemblies.Count > 0)
                {
                    return AssemblyLoadContext.LoadFromAssemblyPath(assemblies[0]);
                }
            }

            return null;
        }
    }
}