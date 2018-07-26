/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using Dolittle.Assemblies;
using Dolittle.Assemblies.Configuration;
using Dolittle.Assemblies.Rules;

namespace Dolittle.Validation
{
    /// <summary>
    /// Reperesents an <see cref="ICanSpecifyAssemblies">assembly specifier</see> for client aspects
    /// </summary>
    public class AssemblySpecifier : ICanSpecifyAssemblies
    {
        /// <inheritdoc/>
        public void Specify(IAssemblyRuleBuilder builder)
        {
            builder.ExcludeAssembliesStartingWith(
                "FluentValidation"
            );
        }
    }
}
