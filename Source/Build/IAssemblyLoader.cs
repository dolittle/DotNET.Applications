using System;
using System.Collections.Generic;
using System.Reflection;
using System.Runtime.Loader;
using Microsoft.Extensions.DependencyModel;

namespace Dolittle.Build
{
    /// <summary>
    /// Represents a system that is capable of loading assemblies out of current <see cref="AssemblyLoadContext"/>
    /// Based on : https://www.codeproject.com/Articles/1194332/Resolving-Assemblies-in-NET-Core
    /// </summary>
    public interface IAssemblyLoader : IDisposable
    {
         /// <summary>
        /// Gets the loaded root assembly
        /// </summary>
        Assembly Assembly { get; }

        /// <summary>
        /// Gets the <see cref="DependencyContext"/> for the <see cref="Assembly"/>
        /// </summary>
        DependencyContext DependencyContext {  get; }

        /// <summary>
        /// Gets the <see cref="AssemblyLoadContext"/> for the <see cref="Assembly"/>
        /// </summary>
        AssemblyLoadContext AssemblyLoadContext {  get; }

        /// <summary>
        /// Get assemblies that are referenced as project references to the loaded assembly
        /// </summary>
        /// <returns>Project <see cref="IEnumerable{Assembly}">assemblies</see></returns>
        IEnumerable<Assembly> GetProjectReferencedAssemblies();
    }
}