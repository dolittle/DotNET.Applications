// Copyright (c) Dolittle. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using Dolittle.DependencyInversion;

namespace Dolittle.Build
{
    /// <summary>
    /// A class providing the startup bindings.
    /// </summary>
    public class BindingProvider : ICanProvideBindings
    {
        /// <inheritdoc/>
        public void Provide(IBindingProviderBuilder builder)
        {
            builder.Bind<ArtifactTypes>().To(new ArtifactTypes());
        }
    }
}