/*---------------------------------------------------------------------------------------------
 *  Copyright (c) 2008-2017 doLittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using doLittle.Assemblies;
using doLittle.Assemblies.Configuration;
using doLittle.Assemblies.Rules;

namespace doLittle
{
    /// <summary>
    /// Reperesents an <see cref="ICanSpecifyAssemblies">assembly specifier</see>
    /// </summary>
    public class AssemblySpecifier : ICanSpecifyAssemblies
    {
        /// <inheritdoc/>
        public void Specify(IAssemblyRuleBuilder builder)
        {
            builder.ExcludeAssembliesStartingWith(
                "System",
                "mscorlib",
                "Microsoft"
            );
        }
    }
}
