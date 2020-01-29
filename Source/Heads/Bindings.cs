// Copyright (c) Dolittle. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using Dolittle.DependencyInversion;

namespace Dolittle.Heads
{
    /// <summary>
    /// Provides bindings related to client.
    /// </summary>
    public class Bindings : ICanProvideBindings
    {
        readonly GetContainer _getContainer;

        /// <summary>
        /// Initializes a new instance of the <see cref="Bindings"/> class.
        /// </summary>
        /// <param name="getContainer"><see cref="GetContainer"/> for getting the correct <see cref="IContainer"/>.</param>
        public Bindings(GetContainer getContainer)
        {
            _getContainer = getContainer;
        }

        /// <inheritdoc/>
        public void Provide(IBindingProviderBuilder builder)
        {
            var head = new Head(Guid.NewGuid());
            builder.Bind<Head>().To(head);
        }
    }
}