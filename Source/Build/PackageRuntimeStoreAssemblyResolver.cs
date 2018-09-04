/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Runtime.InteropServices;
using Dolittle.Collections;
using Microsoft.Extensions.DependencyModel;
using Microsoft.Extensions.DependencyModel.Resolution;

namespace Dolittle.Build
{
    /// <summary>
    /// Represents a <see cref="ICompilationAssemblyResolver"/> that tries to resolve from the package runtime store
    /// </summary>
    /// <remarks>
    /// Read more here : https://docs.microsoft.com/en-us/dotnet/core/deploying/runtime-store
    /// Linux / macOS : /usr/local/share/dotnet/store/{CPU}/{targetFramework e.g. netcoreapp2.0}/{package path}
    /// Windows       : C:/Program Files/dotnet/store/{CPU}/{targetFramework e.g. netcoreapp2.0}/{package path} 
    /// </remarks>
    public class PackageRuntimeStoreAssemblyResolver : ICompilationAssemblyResolver
    {
        /// <inheritdoc/>
        public bool TryResolveAssemblyPaths(CompilationLibrary library, List<string> assemblies)
        {

            var basePath = RuntimeInformation.IsOSPlatform(OSPlatform.Windows)?
                            @"c:\Program Files\dotnet\store":
                            "/usr/local/share/dotnet/store";

            var cpuBasePath = Path.Combine(basePath,RuntimeInformation.ProcessArchitecture.ToString().ToLowerInvariant());
            var found = false;

            // Todo: this should be fixed - can't figure out how we could detect this - it can't be hardcoded like this
            var targetFrameworkBasePath = Path.Combine(cpuBasePath,"netcoreapp2.0");
            var libraryBasePath = Path.Combine(targetFrameworkBasePath,library.Path);
            library.Assemblies.ForEach(assembly => 
            {
                var assemblyPath = Path.Combine(libraryBasePath, assembly);
                if( File.Exists(assemblyPath))
                {
                    assemblies.Add(assemblyPath);
                    found = true;
                }
            });

            return found;
        }
    }
}