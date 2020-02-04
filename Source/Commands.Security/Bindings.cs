// Copyright (c) Dolittle. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using Dolittle.DependencyInversion;
using Dolittle.Runtime.Commands.Security;

namespace Dolittle.Commands.Security
{
    /// <summary>
    /// Bindings required for Commands.Security.
    /// </summary>
    public class Bindings : ICanProvideBindings
    {
        /// <inheritdoc />
        public void Provide(IBindingProviderBuilder builder)
        {
            builder.Bind<ICommandSecurityManager>().To<CommandRequestSecurityManager>().SingletonPerTenant();
        }
    }
}